using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                Clickable c = hit.transform.gameObject.GetComponent<Clickable>();
                if (c != null) {
                    c.OnClick();
                }
            }
        }
        else if (Input.GetButtonUp("Fire1")) 
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Clickable c = hit.transform.gameObject.GetComponent<Clickable>();
                if (c != null)
                {
                    c.OnUnclick();
                }
            }
        }
    }
}
