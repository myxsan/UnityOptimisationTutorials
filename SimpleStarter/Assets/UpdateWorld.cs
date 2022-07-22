﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWorld : MonoBehaviour
{
    [SerializeField] GameObject world;
    Vector2Int lastDrawnPoint = Vector2Int.zero;
    Vector2Int lastErasedPoint = Vector2Int.zero;
    Texture2D texture; //Texture should be set to Read/Write and RGBA32bit in Inspector with no mipmaps
    bool isDrawing = false;
    bool isErasing = false;

    void Start()
    {
        Application.targetFrameRate = 150;
        texture = (Texture2D)Instantiate(world.GetComponent<Renderer>().material.mainTexture);

        //add yellow cells using perlin noise to create some patches of "minerals"
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                if (Mathf.PerlinNoise(x/20.0f,y/20.0f) * 100 < 40)
                  texture.SetPixel(x, y, Color.yellow);
            }
        }

        world.GetComponent<Renderer>().material.mainTexture = texture;
        texture.Apply();
    }

    void Update()
    {
        //record start of mouse drawing (or erasing) to get the first position the mouse touches down
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                lastDrawnPoint = new Vector2Int((int)(ray.textureCoord.x * texture.width),
                                        (int)(ray.textureCoord.y * texture.height));
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            isErasing = true;
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                lastErasedPoint = new Vector2Int((int)(ray.textureCoord.x * texture.width),
                                        (int)(ray.textureCoord.y * texture.height));
            }
        }

        //draw a line between the last known location of the mouse and the current location
        if (Input.GetMouseButton(0))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                world.GetComponent<Renderer>().material.mainTexture = texture;

                DrawPixelLine((int)(ray.textureCoord.x * texture.width),
                                   (int)(ray.textureCoord.y * texture.height),
                                   (int)lastDrawnPoint.x, (int)lastDrawnPoint.y, Color.black, texture);
                texture.Apply();
                lastDrawnPoint = new Vector2Int((int)(ray.textureCoord.x * texture.width),
                                   (int)(ray.textureCoord.y * texture.height));
            }
        }

        if (Input.GetMouseButton(1))
        {
            RaycastHit ray;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray))
            {
                world.GetComponent<Renderer>().material.mainTexture = texture;
                texture.SetPixel((int)(ray.textureCoord.x * texture.width),
                                   (int)(ray.textureCoord.y * texture.height), Color.white);
                texture.Apply();
                lastErasedPoint = new Vector2Int((int)(ray.textureCoord.x * texture.width),
                                   (int)(ray.textureCoord.y * texture.height));
            }
        }

        SimulateWorld(texture);

        if(Input.GetMouseButtonUp(0)) isDrawing = false;
        if(Input.GetMouseButtonUp(1)) isErasing = false;
    }

    int CountNeighbourColor(Vector2Int basePixel, Color col, Texture2D texture)
    {
        int count = 0;
        //loop through all 8 neighbouring cells and if their colour
        //is the one passed through then count it
        for (int ny = -1; ny < 2; ny++)
        {
            for (int nx = -1; nx < 2; nx++)
            {
                if (ny == 0 && nx == 0) continue; //ignore cell you are looking at neighbours
                if (texture.GetPixel(basePixel.x + nx, basePixel.y + ny) == col)
                    count++;
            }
        }
        return count;
    }

    //returns a neighbor list of a particular point
    List<Vector2Int> GetNeighbours(Vector2Int basePixel)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        //loop through all 8 neighbouring cells and add them to neigbors list
        for (int ny = -1; ny < 2; ny++)
        {
            for (int nx = -1; nx < 2; nx++)
            {
                if (ny == 0 && nx == 0) continue; //ignore base cell
                
                neighbors.Add(basePixel + new Vector2Int(nx,ny));
            }
        }
        return neighbors;
    }

    void SimulateWorld(Texture2D texture)
    {
        if(isDrawing)
        {
            foreach(Vector2Int neighbor in GetNeighbours(lastDrawnPoint)) //checks all the neighbors for black cells around them and painting according to it
            {
                //if a cell has more than 4 black neighbours make it blue
                //Commercial Property
                if (CountNeighbourColor(neighbor, Color.black, texture) > 4 )
                {
                    texture.SetPixel(neighbor.x, neighbor.y, Color.blue);
                }
                //if a cell has a black neighbour and is not black itself
                //set to green
                //Residential Property
                else if (CountNeighbourColor(neighbor, Color.black, texture) > 0 && texture.GetPixel(neighbor.x, neighbor.y) == Color.white)
                {
                    texture.SetPixel(neighbor.x, neighbor.y, Color.green);
                }
                //if near a black cell but the cell is already yellow
                //Mining Property
                if (CountNeighbourColor(neighbor, Color.black, texture) > 0 && texture.GetPixel(neighbor.x, neighbor.y) == Color.yellow)
                {
                    texture.SetPixel(neighbor.x, neighbor.y, Color.magenta);
                }       
            }
        }

        if(isErasing)
        {
            foreach(Vector2Int neighbor in GetNeighbours(lastErasedPoint)) // checks all the neighbors of the last erased point and paint them to white
            {
                //if a cell is blue, green or magenta and has no black next to it then it should die = turn white)
                //if road is taken away the cell should die/deallocate property
                if (CountNeighbourColor(neighbor, Color.black, texture) == 0 &&
                    (texture.GetPixel(neighbor.x, neighbor.y) == Color.green ||
                    texture.GetPixel(neighbor.x, neighbor.y) == Color.blue ||
                    texture.GetPixel(neighbor.x, neighbor.y) == Color.magenta))
                {
                    texture.SetPixel(neighbor.x, neighbor.y, Color.white);
                }
            }
        }
        texture.Apply();
    }

    //Draw a pixel by pixel line between two points
    //For more information on the algorithm see: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
    //DO NOT MODIFY OR OPTMISE
    void DrawPixelLine(int x, int y, int x2, int y2, Color color, Texture2D texture)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);
        if (!(longest > shortest))
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            texture.SetPixel(x, y, color);
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
        texture.Apply();
    }
}
