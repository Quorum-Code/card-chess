using System;
using System.Collections;
using System.Collections.Generic;
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

    public bool isSetup = true;

    Camera mainCam;

    Board board;

    int TileLevel = 0;
    int PieceLevel = -1;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        board = new Board(5, 5, 32, 32);
        BuildBoardObjects();

        pieceBuilder.SetGameManager(this);
        pieceBuilder.BuildRook();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log((int)v.x + " " + (int)v.y);
    }

    private void BuildBoardObjects() 
    {
        float xOffset = board.width / 2;
        float yOffset = board.length / 2;

        for (int i = 0; i < board.width; i++) 
        {
            for (int j = 0; j < board.length; j++) 
            {
                GameObject g;
                if ((i + j) % 2 == 0)
                    g = Instantiate(whiteTilePrefab, transform);
                else 
                    g = Instantiate(blackTilePrefab, transform);

                g.transform.localPosition = new Vector3(i - xOffset, j - yOffset, TileLevel);
            }
        }
    }

    public bool SelectPiece(Piece piece) 
    {
        return false;
    }
}
