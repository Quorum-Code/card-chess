using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject whiteTilePrefab;
    [SerializeField] GameObject blackTilePrefab;
    [SerializeField] PieceBuilder pieceBuilder;

    [SerializeField] Color friendlyTile;
    [SerializeField] Color enemyTile;
    [SerializeField] Color friendlyPiece;
    [SerializeField] Color enemyPiece;

    int width;
    int length;
    int maxWidth = 16;
    int maxLength = 16;
    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        BuildBoard(5, 5);
        pieceBuilder.SetGameManager(this);
        pieceBuilder.BuildRook();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildBoard(int _width, int _length) 
    {
        tiles = new Tile[maxWidth, maxLength];

        width = _width;
        length = _length;
        float xOffset = width / 2f - 0.5f;
        float yOffset = length / 2f - 0.5f;

        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < length; j++) 
            {
                GameObject g;
                if ((i + j) % 2 == 0)
                    g = Instantiate(whiteTilePrefab, transform);
                else
                    g = Instantiate(blackTilePrefab, transform);
                g.transform.localPosition = new Vector3(i - xOffset, j - yOffset, 5);
                tiles[i, j] = g.GetComponent<Tile>();
            }
        }

        ColorTiles();
    }

    public void ColorTiles() 
    {
        for (int i = 0; i < width; i++) 
        {
            if (tiles[i, 0] == null)
                Debug.LogError("No tiles?");

            tiles[i, 0].ChangeColor(friendlyTile);
            tiles[i, length-1].ChangeColor(enemyTile);
        }
    }

    public bool TryMovePiece(Piece piece) 
    {
        // Get position as cell value
        int x = (int)(piece.gameObject.transform.position.x + width / 2f); 
        int y = (int)(piece.gameObject.transform.position.y + length / 2f);

        // Check if is within range
        if (x < 0 || x >= width || y < 0 || y >= length)
            return false;

        // Check if cell is moveable to 

        // Remove piece from last tile
        tiles[piece.x, piece.y].piece = null;

        // Move Piece to position
        tiles[x, y].piece = piece;
        piece.transform.position = new Vector3(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y, -5);

        return true;
    } 

    public void SpawnPiece(int x, int y, Piece piece) 
    {

    }

    void SelectPiece(Piece piece) 
    {

    }
}
