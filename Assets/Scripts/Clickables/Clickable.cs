using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnClick(MouseBehavior mb) 
    {
        Debug.Log("OnClick()");
    }

    public virtual void ClickHeld(MouseBehavior mb) 
    {
        Debug.Log("ClickHeld()");
    }

    public virtual void OnUnclick(MouseBehavior mb) 
    {
        Debug.Log("OnUnclick()");
    }
}
