using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceManager
{
    public Sprite[] promotions = new Sprite[4]; // bishop, knight, queen, rook

    public bool moved;

    public override bool Move(int c, int r)
    {
        if (GetMovable(c, r))
        {
            moved = true;
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
        moved = false;
        col = int.Parse(gameObject.name.Substring(1, 1)) - 1;
        row = 1;
        base.Initialize();
    }

    public bool Promotionalble()
    {
        if(row == 0 || row == 7)
            return true;
        return false;
    }

    public void Promotion(int i)
    {
        switch (i)
        {
            case 0:
                gameObject.AddComponent<Bishop>();
                gameObject.GetComponent<Bishop>().Promote(col, row, isPlayer);
                break;
            case 1:
                gameObject.AddComponent<Knight>();
                gameObject.GetComponent<Knight>().Promote(col, row, isPlayer);
                break;
            case 2:
                gameObject.AddComponent<Queen>();
                gameObject.GetComponent<Queen>().Promote(col, row, isPlayer);
                break;
            case 3:
                gameObject.AddComponent<Rook>();
                gameObject.GetComponent<Rook>().Promote(col, row, isPlayer);
                break;
            default:
                gameObject.AddComponent<Queen>();
                gameObject.GetComponent<Queen>().Promote(col, row, isPlayer);
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = promotions[i];
        Destroy(gameObject.GetComponent<Pawn>());
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

        // 공격
        for (int i = -1; i < 2; i += 2)
        {
            if (board.GetBoard(col + i, row + isPlayer) == -isPlayer)
            {
                zone[col + i, row + isPlayer] = true;
            }
            else if (board.GetBoard(col + i, row + isPlayer) < 2)
            {
                zone[col + i, row + isPlayer] = false;
            }
        }
        // 이동
        if (!moved)
        {
            for (int i = 1; i < 3; i++)
            {
                if (board.GetBoard(col, row + i * isPlayer) == 0)
                {
                    zone[col, row + i * isPlayer] = true;
                }
                else if (board.GetBoard(col, row + i) < 2)
                {
                    zone[col, row + i * isPlayer] = false;
                    break;
                }
            }
        }
        else
        {
            if (board.GetBoard(col, row + isPlayer) == 0)
            {
                zone[col, row + isPlayer] = true;
            }
            else if (board.GetBoard(col, row + isPlayer) < 2)
            {
                zone[col, row + isPlayer] = false;
            }
        }
    }
}
