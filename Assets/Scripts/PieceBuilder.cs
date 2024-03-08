using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Move;

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

        p.AddMove(GetRookMove());
    }

    public void SetGameManager(GameManager gm) 
    {
        gameManager = gm;
    }

    public Move GetRookMove() 
    {
        List<Delta> deltas = new List<Delta>
        {
            new Delta(0, 1),
            new Delta(1, 0),
            new Delta(0, -1),
            new Delta(-1, 0)
        };

        Move m = new Move(deltas, MoveType.Many, true, true);
        return m;
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
    public bool isAttack;      // Piece can attack with the move
    public bool isMove;        // Piece can move with the move
    public bool isRelative;    // Piece has special movement forward

    public MoveType moveType;

    public List<Delta> deltas = new List<Delta>();

    public Move(List<Delta> _deltas, MoveType _moveType, bool _isAttack, bool _isMove)
    {
        deltas = _deltas;
        moveType = _moveType;
        isAttack = _isAttack;
        isMove = _isMove;
    }

    public class Delta 
    {
        public int dx { get; private set; }
        public int dy { get; private set; }

        public Delta(int _dx, int _dy) 
        {
            dx = _dx;
            dy = _dy;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Delta))
                return false;
            else 
            {
                Delta d = (Delta)obj;
                return (d.dx == dx && d.dy == dy);
            }
        }

        public override int GetHashCode() 
        {
            return base.GetHashCode();
        }
    }
}
