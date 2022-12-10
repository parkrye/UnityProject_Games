using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChessMain : MonoBehaviour
{
    RaycastHit2D hit;
    public bool pin = false;
    public PieceManager piece;
    public SpriteRenderer pieceSprite;
    public Color original;
    public BoardManager board;
    public GameObject circle;
    GameObject[] prefabs = new GameObject[64];
    int circles = 0;
    public int turn = -1;
    public GameObject barR;
    public GameObject barB;
    public GameObject check;
    public bool inGame = true;
    public GameObject promotionList;
    public bool danger = false;
    public bool useClick = false;
    public GameObject background;
    public GameObject checkMate;
    public bool cm = false;

    private void Start()
    {
        inGame = true;
        check.SetActive(false);
        promotionList.SetActive(false);
        background.SetActive(false);
        checkMate.SetActive(false);
        TurnChanged();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cm)
        {
            if (!useClick)
            {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(mouse, Vector2.zero, 0f);
                if (inGame)
                {
                    CheckedCheck();
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (hit)
                        {
                            if (!pin)
                            {
                                FirstClick();
                            }
                            else
                            {
                                SecondClick(mouse);
                                Promotionable();
                            }
                        }
                    }
                }
                else
                {
                    PromotionState();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void CheckedCheck()
    {
        danger = board.CheckChecked(turn);
        check.SetActive(danger);
        if (danger)
        {
            if (board.CheckMate(turn))
            {
                background.SetActive(true);
                checkMate.SetActive(true);
                cm = true;
            }
        }
    }

    void FirstClick()
    {
        useClick = true;
        if (hit.collider.tag == "Piece")
        {
            piece = hit.transform.gameObject.GetComponent<PieceManager>();
            if (piece.isPlayer == turn)
            {
                pieceSprite = piece.GetComponent<SpriteRenderer>();
                Pin();
            }
        }
        useClick = false;
    }

    void SecondClick(Vector2 mouse)
    {
        {
            useClick = true;
            int col = LeastDiffer(mouse.x, true);
            int row = LeastDiffer(mouse.y, false);

            if(NextSafe(col, row))
            {
                if (hit.collider.tag == "Board")
                {
                    if (piece.Move(col, row))
                    {
                        TurnChanged();
                    }
                }
                else if (hit.collider.tag == "Piece")
                {
                    PieceManager other = hit.transform.gameObject.GetComponent<PieceManager>();
                    if (other.isPlayer != piece.isPlayer)
                    {
                        if (piece.Move(col, row))
                        {
                            other.Die();
                            TurnChanged();
                        }
                    }
                }
            }
            useClick = false;
        }
    }

    bool NextSafe(int col, int row)
    {
        if(piece.GetMovable(col, row))
        {
            int[] prev = piece.GetPos();
            int movedT = board.GetBoard(col, row);
            piece.FakeMove(col, row);
            if (board.CheckChecked(turn))
            {
                piece.FakeMove(prev[0], prev[1]);
                board.ChangePos(col, row, col, row, movedT);
                return false;
            }
            piece.FakeMove(prev[0], prev[1]);
            board.ChangePos(col, row, col, row, movedT);
            return true;
        }
        return false;
    }

    void Promotionable()
    {
        if (piece.GetComponent<Pawn>() != null)
        {
            if (piece.GetComponent<Pawn>().Promotionalble())
            {
                TurnChanged();
                promotionList.SetActive(true);
                inGame = false;
            }
            else
            {
                Pin();
            }
        }
        else
        {
            Pin();
        }
    }

    void PromotionState()
    {
        if (Input.GetMouseButtonUp(0))
        {
            useClick = true;
            if (hit.collider.tag == "PromotionList")
            {
                switch (hit.transform.gameObject.name)
                {
                    case "B":
                        piece.GetComponent<Pawn>().Promotion(0);
                        break;
                    case "N":
                        piece.GetComponent<Pawn>().Promotion(1);
                        break;
                    case "Q":
                        piece.GetComponent<Pawn>().Promotion(2);
                        break;
                    case "R":
                        piece.GetComponent<Pawn>().Promotion(3);
                        break;
                    default:
                        piece.GetComponent<Pawn>().Promotion(2);
                        break;
                }
                TurnChanged();
                promotionList.SetActive(false);
                inGame = true;
                Pin();
            }
            useClick = false;
        }
    }

    void TurnChanged()
    {
        if (turn == 1)
        {
            barR.SetActive(true);
            barB.SetActive(false);
            turn = -1;
        }
        else
        {
            barR.SetActive(false);
            barB.SetActive(true);
            turn = 1;
        }
    }

    void Pin()
    {
        if (pin)
        {
            pieceSprite.color = original;

            for (int i = 0; i < circles; i++)
            {
                Destroy(prefabs[i].gameObject);
            }
            circles = 0;
            pin = false;
        }
        else
        {
            original = pieceSprite.color;
            pieceSprite.color = original * 1.5f;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (piece.GetMovable(i, j))
                    {
                        GameObject prefab = Instantiate(circle, new Vector3(board.GetColPos(i), board.GetRowPos(j), 0f), Quaternion.identity);
                        prefabs[circles] = prefab;
                        circles += 1;
                    }
                }
            }
            pin = true;
        }
    }

    int LeastDiffer(float b, bool cols)
    {
        float least = 20f;
        int index = 0;
        float diff = 0f;

        if (cols)
        {
            for (int i = 0; i < 8; i++)
            {
                if (b > board.GetColPos(i))
                {
                    diff = b - board.GetColPos(i);
                }
                else
                {
                    diff = board.GetColPos(i) - b;
                }
                if (diff < least)
                {
                    index = i;
                    least = diff;
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (b > board.GetRowPos(i))
                {
                    diff = b - board.GetRowPos(i);
                }
                else
                {
                    diff = board.GetRowPos(i) - b;
                }
                if (diff < least)
                {
                    index = i;
                    least = diff;
                }
            }
        }

        return index;
    }
}
