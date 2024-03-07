using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBuilder : MonoBehaviour
{
    [SerializeField] GameObject piecePrefab;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Vector3 pieceSpawn;

    GameManager gameManager;

    public void BuildRook() 
    {
        GameObject g = Instantiate(piecePrefab);
        g.transform.position = pieceSpawn;

        Piece p = g.GetComponent<Piece>();
        p.SetSprite(sprites[0]);
        p.SetGameManager(gameManager);
    }

    public void SetGameManager(GameManager gm) 
    {
        gameManager = gm;
    }
}

public enum MoveType
{
    None,
    Hop,
    Directional,
    Many
}



public class Move
{
    bool isAttack;
    bool isMove;

    MoveType moveType;

    public List<Vector2> deltas = new List<Vector2>();

    public Move(List<Vector2> _deltas, MoveType _moveType, bool _isAttack, bool _isMove)
    {
        deltas = _deltas;
        moveType = _moveType;
        isAttack = _isAttack;
        isMove = _isMove;
    }
}
