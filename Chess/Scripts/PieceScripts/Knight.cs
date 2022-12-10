using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PieceManager
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
            col = 1;
        else
            col = 6;
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

        for (int i = 1; i < 3; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                if (i != j)
                {
                    // 우상으로 이동
                    if (board.GetBoard(col + i, row + j) == isPlayer)
                    {
                        zone[col + i, row + j] = false;
                    }
                    else if (board.GetBoard(col + i, row + j) < 2)
                    {
                        zone[col + i, row + j] = true;
                    }
                    // 좌상으로 이동
                    if (board.GetBoard(col + i, row - j) == isPlayer)
                    {
                        zone[col + i, row - j] = false;
                    }
                    else if (board.GetBoard(col + i, row - j) < 2)
                    {
                        zone[col + i, row - j] = true;
                    }
                    // 우하로 이동
                    if (board.GetBoard(col - i, row + j) == isPlayer)
                    {
                        zone[col - i, row + j] = false;
                    }
                    else if (board.GetBoard(col - i, row + j) < 2)
                    {
                        zone[col - i, row + j] = true;
                    }
                    // 좌하로 이동
                    if (board.GetBoard(col - i, row - j) == isPlayer)
                    {
                        zone[col - i, row - j] = false;
                    }
                    else if (board.GetBoard(col - i, row - j) < 2)
                    {
                        zone[col - i, row - j] = true;
                    }
                }
            }
        }
    }
}
