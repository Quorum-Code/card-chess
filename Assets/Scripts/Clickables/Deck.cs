using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Clickable
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private GameObject cardPrefab;

    public void Start()
    {
        if (handManager == null) {
            Debug.LogError(name + "." + GetType() + " has no handManager");
            Destroy(this);
        }
    }

    public override void OnClick()
    {
        GameObject g = Instantiate(cardPrefab);
        g.transform.position = transform.position;
        handManager.AddCard(g);
    }
}
