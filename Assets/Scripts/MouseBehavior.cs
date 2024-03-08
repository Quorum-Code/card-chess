using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    private Camera mainCamera;
    public GameManager gameManager;

    private Piece piece;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Piece p = hit.transform.gameObject.GetComponent<Piece>();
                if (p != null)
                {
                    piece = p;
                    p.OnClick(this);
                }
            }
        }
        else if (Input.GetButtonUp("Fire1") && piece != null)
        {
            piece.OnUnclick(this);
            piece = null;
        }
        else if (piece != null)
        {
            piece.ClickHeld(this);
        }
    }
}
