using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    Tile[,] tiles;
    public int width { get; private set; }
    public int length { get; private set; }

    int maxWidth;
    int maxLength;

    public Board(int _width, int _length, int _maxWidth, int _maxLength) 
    {
        width = _width;
        length = _length;
        maxWidth = _maxWidth;
        maxLength = _maxLength;

        tiles = new Tile[maxWidth, maxLength];

        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < length; j++) 
            {
                tiles[i, j] = new Tile(i, j);
            }
        }
    }

    public bool isInBounds((int, int) point) 
    {
        if (point.Item1 >= 0 && point.Item1 < width && point.Item2 >= 0 && point.Item2 < length)
            return true;
        return false;
    }

    public class Tile 
    {
        int x;
        int y;
        Piece piece;
        GameObject go;

        public Tile(int _x, int _y) 
        {
            x = _x;
            y = _y;
        }
    }
}
