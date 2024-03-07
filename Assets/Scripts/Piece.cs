using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Clickable
{
    string Name;

    public GameManager gameManager;

    public int x = 0;
    public int y = 0;

    bool hasMoved = false;
    Move move;

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

    public override void OnClick()
    {
        if (animator != null)
            StopCoroutine(animator);
        animator = FollowMouse();
        StartCoroutine(animator);
    }

    public override void OnUnclick() 
    {
        // Try to place piece in world space
        if (animator != null)
            StopCoroutine(animator);
        gameManager.TryMovePiece(this);
    }

    public IEnumerator FollowMouse() 
    {
        Camera main = Camera.main;

        while (true) 
        {
            gameObject.transform.position = main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            yield return null;
        }
    }
}
