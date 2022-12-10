using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform[] boardCol = new Transform[8];
    public Transform[] boardRow = new Transform[8];
    public int[,] board = new int[8, 8];
    public GameObject blue;
    public GameObject red;
    public PieceManager[] bPieces;
    public PieceManager[] rPieces;

    private void Start()
    {
        bPieces = blue.GetComponentsInChildren<PieceManager>();
        rPieces = red.GetComponentsInChildren<PieceManager>();
    }

    public bool CheckMate(int flag)
    {
        if (flag == 1)
        {
            for (int i = 0; i < bPieces.Length; i++)
            {
                for(int c = 0; c < 8; c++)
                {
                    for(int r = 0; r < 8; r++)
                    {
                        if(bPieces[i].GetMovable(c, r))
                        {
                            int[] prev = bPieces[i].GetPos();
                            int movedT = board[c, r];
                            bPieces[i].FakeMove(c, r);
                            if (!CheckChecked(flag))
                            {
                                rPieces[i].FakeMove(prev[0], prev[1]);
                                ChangePos(c, r, c, r, movedT);
                                return false;
                            }
                            bPieces[i].FakeMove(prev[0], prev[1]);
                            ChangePos(c, r, c, r, movedT);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < rPieces.Length; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    for (int r = 0; r < 8; r++)
                    {
                        if (rPieces[i].GetMovable(c, r))
                        {
                            int[] prev = rPieces[i].GetPos();
                            int movedT = board[c, r];
                            rPieces[i].FakeMove(c, r);
                            if (!CheckChecked(flag))
                            {
                                rPieces[i].FakeMove(prev[0], prev[1]);
                                ChangePos(c, r, c, r, movedT);
                                return false;
                            }
                            rPieces[i].FakeMove(prev[0], prev[1]);
                            ChangePos(c, r, c, r, movedT);
                        }
                    }
                }
            }
        }

        return true;
    }

    public bool CheckChecked(int flag)
    {
        Renewal();

        bool check = false;

        if (flag == 1)
        {
            int[] kPos = new int[2];
            for(int i = 0; i < bPieces.Length; i++)
            {
                if (bPieces[i].name.StartsWith("K"))
                {
                    kPos = bPieces[i].GetPos();
                    break;
                }
            }

            for (int i = 0; i < rPieces.Length; i++)
            {
                if (rPieces[i].GetMovable(kPos[0], kPos[1]))
                {
                    check = true;
                }
            }
        }
        else
        {
            int[] kPos = new int[2];
            for (int i = 0; i < rPieces.Length; i++)
            {
                if (rPieces[i].name.StartsWith("K"))
                {
                    kPos = rPieces[i].GetPos();
                    break;
                }
            }

            for (int i = 0; i < bPieces.Length; i++)
            {
                if (bPieces[i].GetMovable(kPos[0], kPos[1]))
                {
                    Debug.Log(bPieces[i].name + " is Attack");
                    check = true;
                }
            }
        }
        return check;
    }

    public void Renewal()
    {
        for (int i = 0; i < bPieces.Length; i++)
        {
            bPieces[i].MoveRule();
        }

        for (int i = 0; i < rPieces.Length; i++)
        {
            rPieces[i].MoveRule();
        }
    }

    public float GetColPos(int col)
    {
        return boardCol[col].position.x;
    }

    public float GetRowPos(int row)
    {
        return boardRow[row].position.y;
    }

    public void ChangePos(int c1, int r1, int c2, int r2, int p)
    {
        board[c1, r1] = 0;
        board[c2, r2] = p;
    }

    public int GetBoard(int c, int r)
    {
        if (c >= 0 && c < 8 && r >= 0 && r < 8)
            return board[c, r];
        else
            return 9;
    }
}
