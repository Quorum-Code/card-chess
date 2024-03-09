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
        board = new Board(this, 5, 5, 32, 32);
        BuildBoardObjects();

        pieceBuilder.SetGameManager(this);
        pieceBuilder.BuildRook();
    }

    private void Update()
    {
        if (!isSetup) 
        {
            board.isSetup = false;
            isSetup = true;
        }
    }

    public void UpdatePieceObject(Piece piece)
    {
        
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

    public (int, int) MouseToBoardPos() 
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        float fx = mousePos.x + board.width / 2f;
        float fy = mousePos.y + board.length / 2f;

        if (fx < 0 || fy < 0)
            return (-1, -1);
        else 
            return ((int) fx, (int) fy);
    }

    public bool MovePiece(Piece piece) 
    {
        (int, int) pos = MouseToBoardPos();

        if (board.isInBounds(pos))
            return board.MovePiece(piece, pos);
        else return false;
    }

    public void KillPiece(Piece piece)
    {
        Debug.Log("KilledPiece!");
    }

    public bool SelectPiece(Piece piece) 
    {
        return false;
    }
}
