using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    List<CardObject> cardObjects = new List<CardObject>();
    [SerializeField] private float handWidth = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(GameObject go) {
        go.transform.SetParent(this.transform);
        cardObjects.Add(new CardObject(go));
        RefreshCardPositions();
    }

    private void RefreshCardPositions() 
    {
        float start = handWidth / -2f;
        float chunk = handWidth / cardObjects.Count; 
        for (int i = 0; i < cardObjects.Count; i++) 
        {
            if (cardObjects[i].coroutine != null)
                StopCoroutine(cardObjects[i].coroutine);

            cardObjects[i].coroutine = cardObjects[i].Moving(new Vector3(start + chunk * i, 0, 0));
            StartCoroutine(cardObjects[i].coroutine);
        }
    }

    protected enum CardState { 
        None, 
        Drawing,
        Selected
    }

    protected class CardObject {
        public GameObject gameObject;
        public IEnumerator coroutine;
        public CardState cardState;

        public CardObject(GameObject go) {
            gameObject = go;
        }

        public bool ChangeState(CardState cs) {
            if (cardState == CardState.None && cs == CardState.Drawing)
            {
                return true;
            }
            else if (cardState == CardState.Drawing && cs == CardState.None) 
            { 
                return true;
            }

            return false;
        }

        public IEnumerator Moving(Vector3 newPos) 
        {
            Vector3 oldPos = gameObject.transform.localPosition;
            float t = 0f;
            float end = .5f;
            while (t < end) 
            {
                gameObject.transform.localPosition = Vector3.Lerp(oldPos, newPos, t / end);
                t += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.localPosition = newPos;
        }
    }
}
