using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : PieceManager
{
    public override bool Move(int c, int r)
    {
        if (GetMovable(c, r))
        {
            SetPosition(c, r);
            MoveRule();
            return true;
        }
        return false;
    }

    public override bool GetMovable(int c, int r)
    {
        MoveRule();
        return zone[c, r];
    }

    public override void Initialize()
    {
        if (int.Parse(gameObject.name.Substring(1, 1)) == 1)
            col = 2;
        else
            col = 5;
        row = 0;
        base.Initialize();
    }

    public override void MoveRule()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                zone[i, j] = false;
            }
        }

        // 우상으로 이동
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col + i, row + i) == 0)
            {
                zone[col + i, row + i] = true;
            }
            else if (board.GetBoard(col + i, row + i) == isPlayer)
            {
                zone[col + i, row + i] = false;
                break;
            }
            else if (board.GetBoard(col + i, row + i) == -isPlayer)
            {
                zone[col + i, row + i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // 좌상으로 이동
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col + i, row - i) == 0)
            {
                zone[col + i, row - i] = true;
            }
            else if (board.GetBoard(col + i, row - i) == isPlayer)
            {
                zone[col + i, row - i] = false;
                break;
            }
            else if (board.GetBoard(col + i, row - i) == -isPlayer)
            {
                zone[col + i, row - i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // 우하로 이동
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col - i, row + i) == 0)
            {
                zone[col - i, row + i] = true;
            }
            else if (board.GetBoard(col - i, row + i) == isPlayer)
            {
                zone[col - i, row + i] = false;
                break;
            }
            else if (board.GetBoard(col - i, row + i) == -isPlayer)
            {
                zone[col - i, row + i] = true;
                break;
            }
            else
            {
                break;
            }
        }
        // 좌하로 이동
        for (int i = 1; i < 8; i++)
        {
            if (board.GetBoard(col - i, row - i) == 0)
            {
                zone[col - i, row - i] = true;
            }
            else if (board.GetBoard(col - i, row - i) == isPlayer)
            {
                zone[col - i, row - i] = false;
                break;
            }
            else if (board.GetBoard(col - i, row - i) == -isPlayer)
            {
                zone[col - i, row - i] = true;
                break;
            }
            else
            {
                break;
            }
        }
    }
}
