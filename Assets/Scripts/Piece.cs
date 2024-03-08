using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Clickable
{
    string Name;

    public GameManager gameManager;

    public int x = -1;
    public int y = -1;

    bool hasMoved = false;
    public List<Move> moves = new List<Move>();
    IEnumerator animator;

    public void SetSprite(Sprite s) 
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) 
        {
            sr.sprite = s;
        }
    }

    public void SetGameManager(GameManager gm) 
    {
        gameManager = gm;
    }

    public override void OnClick(MouseBehavior mb)
    {
        if (animator != null)
            StopCoroutine(animator);
        animator = FollowMouse();
        StartCoroutine(animator);
    }

    public override void OnUnclick(MouseBehavior mb) 
    {
        // Try to place piece in world space
        if (animator != null)
            StopCoroutine(animator);
        if (gameManager == null)
        {
            Debug.LogError("No game manager???");
        }
        else 
        {
            //gameManager.TryMovePiece(this);
        }
    }

    public override void ClickHeld(MouseBehavior mb)
    {

    }

    public IEnumerator FollowMouse() 
    {
        Camera main = Camera.main;
        Vector2 initMousePos = main.ScreenToWorldPoint(Input.mousePosition);
        while (((Vector2)main.ScreenToWorldPoint(Input.mousePosition) - initMousePos).magnitude < 0.2f) 
        {
            yield return null;
        }

        while (true) 
        {
            gameObject.transform.position = main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            yield return null;
        }
    }

    public void AddMove(Move move)
    {
        moves.Add(move);
    }

    public List<(int, int)> GetValidMoves(Board board)
    {
        List<(int, int)> positions = new List<(int, int)>();

        // for each move
        foreach (Move move in moves) 
        {
            // for each delta
            foreach (Move.Delta d in move.deltas) 
            {
                int dx = x + d.dx;
                int dy = y + d.dy;

                bool isContinue = true;
                while (isContinue) 
                {
                    isContinue = false;

                    if (move.moveType == MoveType.Many)
                    {
                        if (board.isInBounds((dx, dy)))
                        {
                            positions.Add((dx, dy));
                            isContinue = true;

                            dx += d.dx;
                            dy += d.dy;
                        }
                    }
                    else 
                    {
                        if (board.isInBounds((dx, dy))) 
                        {
                            positions.Add((dx, dy));
                        }
                    }
                }
            }
        }

        return positions;
    }
}
