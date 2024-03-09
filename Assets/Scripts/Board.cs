using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    GameManager gm;
    Tile[,] tiles;
    public bool isSetup = true;
    public int width { get; private set; }
    public int length { get; private set; }

    int maxWidth;
    int maxLength;

    public Board(GameManager _gm, int _width, int _length, int _maxWidth, int _maxLength) 
    {
        gm = _gm;
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

        Debug.Log("Tile[0, 0]: " + tiles[0, 0] == null);
    }

    public bool isInBounds((int, int) point) 
    {
        if (point.Item1 >= 0 && point.Item1 < width && point.Item2 >= 0 && point.Item2 < length)
            return true;
        return false;
    }

    public bool isOccupied((int, int) pos) 
    {
        if (isInBounds(pos) && tiles[pos.Item1, pos.Item2].piece == null) 
        {
            return false;
        }
        return true;
    }

    public bool MovePiece(Piece piece, (int, int) pos) 
    {
        if (!isInBounds(pos))
            return false;

        if (isSetup && !isOccupied(pos))
        {
            if (isInBounds((piece.x, piece.y))) 
            {
                Debug.Log(piece.x + " " + piece.y);
                tiles[piece.x, piece.y].piece = null;
            }

            tiles[pos.Item1, pos.Item2].piece = piece;

            piece.x = pos.Item1;
            piece.y = pos.Item2;

            Vector2 v2 = new Vector2(piece.x + 0.5f - width / 2f, piece.y + 0.5f - length / 2f );

            piece.gameObject.transform.position = v2;

            return true;
        }
        else if (!isSetup) 
        {
            // Game logic here
            // (int, int) needs to be in possible moves
            if (PossibleMoves(piece).Contains(pos)) 
            {
                Debug.Log("Can move here!");
                if (isInBounds((piece.x, piece.y)))
                    tiles[piece.x, piece.y].piece = null;

                piece.x = pos.Item1;
                piece.y = pos.Item2;

                tiles[piece.x, piece.y].piece = piece;

                Vector2 v2 = new Vector2(piece.x + 0.5f - width / 2f, piece.y + 0.5f - length / 2f);

                piece.gameObject.transform.position = v2;
                return true;
            }
            else 
            {
                // Move piece back to origianl position

                Debug.Log("Can't move here...");

                Vector2 v2 = new Vector2(piece.x + 0.5f - width / 2f, piece.y + 0.5f - length / 2f);

                piece.gameObject.transform.position = v2;
                return true;
            }
        }

        return false;
    }

    public List<(int, int)> PossibleMoves(Piece piece) 
    {
        List<(int, int)> points = new List<(int, int)> ();

        foreach (Move move in piece.moves) 
        {
            foreach (Move.Delta delta in move.deltas) 
            {
                if (move.moveType == MoveType.Many) 
                {
                    points.AddRange(GetManyMoves(piece, move, delta));
                }
            }
        }

        return points;
    }

    private List<(int, int)> GetManyMoves(Piece piece, Move move, Move.Delta delta) 
    {
        List<(int, int)> list = new List<(int, int)> ();

        int dx = piece.x + delta.dx;
        int dy = piece.y + delta.dy;

        bool isValid = true;
        while (isValid) 
        {
            if (!isInBounds((dx, dy)))
            {
                return list;
            }
            else if (isOccupied((dx, dy)))
            {
                if (move.isAttack)
                    list.Add((dx, dy));
                return list;
            }
            else 
            {
                list.Add((dx, dy));
            }

            dx += delta.dx;
            dy += delta.dy;
        }

        return list;
    }

    public class Tile 
    {
        int x;
        int y;
        public Piece piece;
        GameObject go;

        public Tile(int _x, int _y) 
        {
            x = _x;
            y = _y;
        }
    }
}
