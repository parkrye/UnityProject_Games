using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public BoardManager board;
    public int col;
    public int row;
    public int isPlayer;    // 1:플레이어, -1:적
    public bool[,] zone;

    public virtual bool Move(int c, int r)
    {
        return true;
    }

    public virtual bool GetMovable(int c, int r)
    {
        return true;
    }

    public virtual bool[,] GetZone()
    {
        return zone;
    }

    public virtual void MoveRule()
    {
        
    }

    public void Die()
    {
        isPlayer = 0;
        gameObject.tag = "Board";
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public int[] GetPos()
    {
        return new int[] { col, row };
    }

    public virtual void Initialize()
    {
        board = GameObject.FindWithTag("Board").GetComponent<BoardManager>();
        zone = new bool[8, 8];
        if (isPlayer == -1)
        {
            row = 7 - row;
        }
        SetPosition(col, row);
    }

    public void Promote(int c, int r, int p)
    {
        board = GameObject.FindWithTag("Board").GetComponent<BoardManager>();
        zone = new bool[8, 8];
        col = c;
        row = r;
        isPlayer = p;
        SetPosition(c, r);
        MoveRule();
        gameObject.transform.position = new Vector3(board.GetColPos(col), board.GetRowPos(row), 1f);
    }

    public void FakeMove(int c, int r)
    {
        board.ChangePos(col, row, c, r, isPlayer);
        col = c;
        row = r;
    }

    public void SetPosition(int c, int r)
    {
        board.ChangePos(col, row, c, r, isPlayer);
        col = c;
        row = r;
        gameObject.transform.position = new Vector3(board.GetColPos(col), board.GetRowPos(row), 1f);
    }
}