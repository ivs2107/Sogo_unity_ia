using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    // Start is called before the first frame update
    public int Width { get; set; }
    public int Height { get; set; }

    public int Depth { get; set; }

    //  public USCell[,] TabUSCell;

    public bool tour = true;


    public string[,,] tabMap;


    public Map(int width, int height, int depth )
    {
        Width = width;
        Height = height;
        Depth = depth;
        tabMap = new string[Height, Width, Depth];
       // TabUSCell = new USCell[Height, Width];
        for (int floor = 0; floor < Width; floor++)
        {
            for (int col = 0; col < Height; col++)
            {
                for (int row = 0; row < Depth; row++)
                {
                    tabMap[floor,col,row] = ".";
                }
            }
        }
    }
}
