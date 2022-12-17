using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    RaycastHit2D hit;  // On Mouse Hit
    GameObject target; // On Mouse Target
    bool handling;     // true: can move, false: script is processing
    int turn;          // 0 to 7. 8 is end
    int[] board;       // 0 ~ 8. 0: Empty, 1: X Mark, 2: O Mark
    public GameObject checkLine;    // Tic Tac Toe Line
    public TimerManager timer;      // If Game is End, Stop Timer
    public GameObject[] turnTable = new GameObject[2];  // Who's Turn Table
    int end;           // 0: Not, 1: Who is Win, 2: Draw Game
    public int mode;   // 0: 2 Players, 1: Lv1 Computer, 2: Lv2 Computer, 3: Lv3 Computer
    int playerTurn;    // default = 0
    public GameObject block; // For blocks
    GameObject[] blocks;     // Tic Tac Toe Blocks
    public GameObject[] marks = new GameObject[2];      // Turn Marks

    // Start is called before the first frame update
    void Start()
    {
        handling = false;
        turn = 0;
        board = new int[9];
        end = 0;
        mode = PlayerPrefs.GetInt("Mode");
        playerTurn = PlayerPrefs.GetInt("Turn");
        turnTable[0].GetComponent<SpriteRenderer>().color = Color.black;
        turnTable[1].GetComponent<SpriteRenderer>().color = Color.grey;
        blocks = new GameObject[9];
        Transform[] tmp = block.GetComponentsInChildren<Transform>();
        int j = 0;
        for(int i = 0; i < tmp.Length; i++)
        {
            if(tmp[i].gameObject.tag == "Block")
            {
                blocks[j] = tmp[i].gameObject;
                j++;
            }
        }
        if(mode > 0)
        {
            if(playerTurn == 0)
            {
                marks[0].SetActive(true);
            }
            else
            {
                marks[1].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (end > 0)
        {
            SceneChanger();
        }
        else
        {
            if (!handling)
            {
                if (mode == 0)
                {
                    PlayerMove();
                }
                else
                {
                    if (Turn())
                    {
                        PlayerMove();
                    }
                    else
                    {
                        switch (mode)
                        {
                            default:
                            case 1:
                                StartCoroutine(ComputerLv1());
                                break;
                            case 2:
                                StartCoroutine(ComputerLv2());
                                break;
                            case 3:
                                StartCoroutine(ComputerLv3());
                                break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator ComputerLv1()
    {
        handling = true;
        yield return new WaitForSeconds((8 - turn) * 0.1f);
        int move = Random.Range(0, 9);
        while(board[move] != 0)
        {
            yield return new WaitForSeconds((8 - turn) * 0.05f);
            move = Random.Range(0, 9);
        }
        ComputerClick(move);
        Check();
    }

    IEnumerator ComputerLv2()
    {
        handling = true;
        int move = 0;
        bool win = false;
        bool danger = false;
        if (!win)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i * 3] == board[i * 3 + 1]) && board[i * 3] == 2 + playerTurn && board[i * 3 + 2] == 0)
                {
                    win = true;
                    move = i * 3 + 2;
                    break;
                }
                else if ((board[i * 3] == board[i * 3 + 2]) && board[i * 3] == 2 + playerTurn && board[i * 3 + 1] == 0)
                {
                    win = true;
                    move = i * 3 + 1;
                    break;
                }
                else if ((board[i * 3 + 1] == board[i * 3 + 2]) && board[i * 3 + 1] == 2 + playerTurn && board[i * 3] == 0)
                {
                    win = true;
                    move = i * 3;
                    break;
                }
                else if ((board[i] == board[i + 3]) && board[i] == 2 + playerTurn && board[i + 6] == 0)
                {
                    win = true;
                    move = i + 6;
                    break;
                }
                else if ((board[i] == board[i + 6]) && board[i] == 2 + playerTurn && board[i + 3] == 0)
                {
                    win = true;
                    move = i + 3;
                    break;
                }
                else if ((board[i + 3] == board[i + 6]) && board[i + 3] == 2 + playerTurn && board[i] == 0)
                {
                    win = true;
                    move = i;
                    break;
                }
            }
            if ((board[0] == board[4]) && board[0] == 2 + playerTurn && board[8] == 0)
            {
                win = true;
                move = 8;
            }
            else if ((board[0] == board[8]) && board[0] == 2 + playerTurn && board[4] == 0)
            {
                win = true;
                move = 4;
            }
            else if ((board[4] == board[8]) && board[4] == 2 + playerTurn && board[0] == 0)
            {
                win = true;
                move = 0;
            }
            else if ((board[2] == board[4]) && board[2] == 2 + playerTurn && board[6] == 0)
            {
                win = true;
                move = 6;
            }
            else if ((board[2] == board[6]) && board[2] == 2 + playerTurn && board[4] == 0)
            {
                win = true;
                move = 4;
            }
            else if ((board[4] == board[6]) && board[4] == 2 + playerTurn && board[2] == 0)
            {
                win = true;
                move = 2;
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (!win)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i * 3] == board[i * 3 + 1]) && board[i * 3] == 1 + playerTurn && board[i * 3 + 2] == 0)
                {
                    danger = true;
                    move = i * 3 + 2;
                    break;
                }
                else if ((board[i * 3] == board[i * 3 + 2]) && board[i * 3] == 1 + playerTurn && board[i * 3 + 1] == 0)
                {
                    danger = true;
                    move = i * 3 + 1;
                    break;
                }
                else if ((board[i * 3 + 1] == board[i * 3 + 2]) && board[i * 3 + 1] == 1 + playerTurn && board[i * 3] == 0)
                {
                    danger = true;
                    move = i * 3;
                    break;
                }
                else if ((board[i] == board[i + 3]) && board[i] == 1 + playerTurn && board[i + 6] == 0)
                {
                    danger = true;
                    move = i + 6;
                    break;
                }
                else if ((board[i] == board[i + 6]) && board[i] == 1 + playerTurn && board[i + 3] == 0)
                {
                    danger = true;
                    move = i + 3;
                    break;
                }
                else if ((board[i + 3] == board[i + 6]) && board[i + 3] == 1 + playerTurn && board[i] == 0)
                {
                    danger = true;
                    move = i;
                    break;
                }
            }
            if ((board[0] == board[4]) && board[0] == 1 + playerTurn && board[8] == 0)
            {
                danger = true;
                move = 8;
            }
            else if ((board[0] == board[8]) && board[0] == 1 + playerTurn && board[4] == 0)
            {
                danger = true;
                move = 4;
            }
            else if ((board[4] == board[8]) && board[4] == 1 + playerTurn && board[0] == 0)
            {
                danger = true;
                move = 0;
            }
            else if ((board[2] == board[4]) && board[2] == 1 + playerTurn && board[6] == 0)
            {
                danger = true;
                move = 6;
            }
            else if ((board[2] == board[6]) && board[2] == 1 + playerTurn && board[4] == 0)
            {
                danger = true;
                move = 4;
            }
            else if ((board[4] == board[6]) && board[4] == 1 + playerTurn && board[2] == 0)
            {
                danger = true;
                move = 2;
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (!win && !danger)
        {
            move = Random.Range(0, 9);
            while (board[move] != 0)
            {
                yield return new WaitForSeconds((8 - turn) * 0.05f);
                move = Random.Range(0, 9);
            }
        }
        ComputerClick(move);
        Check();
    }

    IEnumerator ComputerLv3()
    {
        handling = true;
        int move = 0;
        bool win = false;
        bool danger = false;
        bool side = false;
        if(!win)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i * 3] == board[i * 3 + 1]) && board[i * 3] == 2 + playerTurn && board[i * 3 + 2] == 0)
                {
                    win = true;
                    move = i * 3 + 2;
                    break;
                }
                else if ((board[i * 3] == board[i * 3 + 2]) && board[i * 3] == 2 + playerTurn && board[i * 3 + 1] == 0)
                {
                    win = true;
                    move = i * 3 + 1;
                    break;
                }
                else if ((board[i * 3 + 1] == board[i * 3 + 2]) && board[i * 3 + 1] == 2 + playerTurn && board[i * 3] == 0)
                {
                    win = true;
                    move = i * 3;
                    break;
                }
                else if ((board[i] == board[i + 3]) && board[i] == 2 + playerTurn && board[i + 6] == 0)
                {
                    win = true;
                    move = i + 6;
                    break;
                }
                else if ((board[i] == board[i + 6]) && board[i] == 2 + playerTurn && board[i + 3] == 0)
                {
                    win = true;
                    move = i + 3;
                    break;
                }
                else if ((board[i + 3] == board[i + 6]) && board[i + 3] == 2 + playerTurn && board[i] == 0)
                {
                    win = true;
                    move = i;
                    break;
                }
            }
            if ((board[0] == board[4]) && board[0] == 2 + playerTurn && board[8] == 0)
            {
                win = true;
                move = 8;
            }
            else if ((board[0] == board[8]) && board[0] == 2 + playerTurn && board[4] == 0)
            {
                win = true;
                move = 4;
            }
            else if ((board[4] == board[8]) && board[4] == 2 + playerTurn && board[0] == 0)
            {
                win = true;
                move = 0;
            }
            else if ((board[2] == board[4]) && board[2] == 2 + playerTurn && board[6] == 0)
            {
                win = true;
                move = 6;
            }
            else if ((board[2] == board[6]) && board[2] == 2 + playerTurn && board[4] == 0)
            {
                win = true;
                move = 4;
            }
            else if ((board[4] == board[6]) && board[4] == 2 + playerTurn && board[2] == 0)
            {
                win = true;
                move = 2;
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (!win)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i * 3] == board[i * 3 + 1]) && board[i * 3] == 1 + playerTurn && board[i * 3 + 2] == 0)
                {
                    danger = true;
                    move = i * 3 + 2;
                    break;
                }
                else if ((board[i * 3] == board[i * 3 + 2]) && board[i * 3] == 1 + playerTurn && board[i * 3 + 1] == 0)
                {
                    danger = true;
                    move = i * 3 + 1;
                    break;
                }
                else if ((board[i * 3 + 1] == board[i * 3 + 2]) && board[i * 3 + 1] == 1 + playerTurn && board[i * 3] == 0)
                {
                    danger = true;
                    move = i * 3;
                    break;
                }
                else if ((board[i] == board[i + 3]) && board[i] == 1 + playerTurn && board[i + 6] == 0)
                {
                    danger = true;
                    move = i + 6;
                    break;
                }
                else if ((board[i] == board[i + 6]) && board[i] == 1 + playerTurn && board[i + 3] == 0)
                {
                    danger = true;
                    move = i + 3;
                    break;
                }
                else if ((board[i + 3] == board[i + 6]) && board[i + 3] == 1 + playerTurn && board[i] == 0)
                {
                    danger = true;
                    move = i;
                    break;
                }
            }
            if ((board[0] == board[4]) && board[0] == 1 + playerTurn && board[8] == 0)
            {
                danger = true;
                move = 8;
            }
            else if ((board[0] == board[8]) && board[0] == 1 + playerTurn && board[4] == 0)
            {
                danger = true;
                move = 4;
            }
            else if ((board[4] == board[8]) && board[4] == 1 + playerTurn && board[0] == 0)
            {
                danger = true;
                move = 0;
            }
            else if ((board[2] == board[4]) && board[2] == 1 + playerTurn && board[6] == 0)
            {
                danger = true;
                move = 6;
            }
            else if ((board[2] == board[6]) && board[2] == 1 + playerTurn && board[4] == 0)
            {
                danger = true;
                move = 4;
            }
            else if ((board[4] == board[6]) && board[4] == 1 + playerTurn && board[2] == 0)
            {
                danger = true;
                move = 2;
            }
            yield return new WaitForSeconds(0.5f);
        }
        if(!win && !danger)
        {
            int comCount = 0;
            int plaCount = 0;
            if(board[0] == 0)
            {
                for(int i = 1; i < 3; i++)
                {
                    if(board[i] == 2 + playerTurn || board[i * 3] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if(board[i] == 1 + playerTurn || board[i * 3] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if(comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[1] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 2] == 2 + playerTurn || board[i * 3 + 4] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 2] == 1 + playerTurn || board[i * 3 + 4] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[2] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i] == 2 + playerTurn || board[i * 3 + 5] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i] == 1 + playerTurn || board[i * 3 + 5] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[3] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 6] == 2 + playerTurn || board[i + 4] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 6] == 1 + playerTurn || board[i + 4] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[4] == 0)
            {
                int i = 0;
                int j = 0;
                while(i < 9 && j < 9)
                {
                    if (i != j && i != 4 && j != 4)
                    {
                        if (board[i] == board[j] && board[i] != 0)
                        {
                            move = 0;
                            side = true;
                            break;
                        }
                    }
                    j++;
                    if(j > 8)
                    {
                        j = 0;
                        i++;
                    }
                }
            }
            else if (board[5] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 6 + 2] == 2 + playerTurn || board[i + 3] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 6 + 2] == 1 + playerTurn || board[i + 3] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[6] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 3] == 2 + playerTurn || board[i + 7] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 3] == 1 + playerTurn || board[i + 7] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[7] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 2 + 6] == 2 + playerTurn || board[i * 3 + 1] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 2 + 6] == 1 + playerTurn || board[i * 3 + 1] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
            else if (board[8] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (board[i * 3 + 2] == 2 + playerTurn || board[i + 6] == 2 + playerTurn)
                    {
                        comCount++;
                    }
                    else if (board[i * 3 + 2] == 1 + playerTurn || board[i + 6] == 1 + playerTurn)
                    {
                        plaCount++;
                    }
                }
                if (comCount >= 2 || plaCount >= 2)
                {
                    move = 0;
                    side = true;
                }
            }
        }
        if (!win && !danger && !side)
        {
            move = Random.Range(0, 9);
            while (board[move] != 0)
            {
                yield return new WaitForSeconds((8 - turn) * 0.05f);
                move = Random.Range(0, 9);
            }
        }
        ComputerClick(move);
        Check();
    }

    // Computer's Click Action
    void ComputerClick(int move)
    {
        board[move] = 2 + playerTurn;
        blocks[move].transform.GetChild(1 - playerTurn).gameObject.SetActive(true);
        turnTable[playerTurn].GetComponent<SpriteRenderer>().color = Color.black;
        turnTable[1 - playerTurn].GetComponent<SpriteRenderer>().color = Color.grey;
        blocks[move].GetComponent<BoxCollider2D>().enabled = false;
    }

    void PlayerMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
                if (target.tag == "Block")
                {
                    handling = true;
                    PlayerClick();
                    Check();
                }
            }
        }
    }

    // Player's Click Action
    void PlayerClick()
    {
        if (Turn())
        {
            board[int.Parse(target.name.Substring(1))] = 1 + playerTurn;
            target.transform.GetChild(playerTurn).gameObject.SetActive(true);
            turnTable[playerTurn].GetComponent<SpriteRenderer>().color = Color.grey;
            turnTable[1 - playerTurn].GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            board[int.Parse(target.name.Substring(1))] = 2 + playerTurn;
            target.transform.GetChild(1 - playerTurn).gameObject.SetActive(true);
            turnTable[1 - playerTurn].GetComponent<SpriteRenderer>().color = Color.grey;
            turnTable[playerTurn].GetComponent<SpriteRenderer>().color = Color.black;
        }
        target.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Tic Tac Toe Check
    void Check()
    {
        bool victory = false;
        for (int i = 0; i < 3; i++)
        {
            if (board[i * 3] == board[i * 3 + 1] && board[i * 3] == board[i * 3 + 2] && board[i * 3] != 0)
            {
                victory = true;
                GameObject rowCL = Instantiate(checkLine);
                rowCL.transform.rotation = Quaternion.Euler(0, 0, 90);
                rowCL.transform.localPosition = new Vector3(0, 2 - i * 2, -2);
            }
            if (board[i] == board[i + 3] && board[i] == board[i + 6] && board[i] != 0)
            {
                victory = true;
                GameObject colCL = Instantiate(checkLine);
                colCL.transform.localPosition = new Vector3(-2 + i * 2, 0, -2);
            }
        }
        if (board[0] == board[4] && board[0] == board[8] && board[0] != 0)
        {
            victory = true;
            GameObject x1CL = Instantiate(checkLine);
            x1CL.transform.rotation = Quaternion.Euler(0, 0, 45);
            x1CL.transform.localScale = new Vector3(0.3f, 1.5f, 1f);
        }
        if (board[2] == board[4] && board[2] == board[6] && board[2] != 0)
        {
            victory = true;
            GameObject x2CL = Instantiate(checkLine);
            x2CL.transform.rotation = Quaternion.Euler(0, 0, -45);
            x2CL.transform.localScale = new Vector3(0.3f, 1.5f, 1f);
        }

        if (victory)
        {
            end = 1;
            timer.StopClock();
            if (Turn())
            {
                turnTable[playerTurn].GetComponent<SpriteRenderer>().color = Color.yellow;
                turnTable[1 - playerTurn].GetComponent<SpriteRenderer>().color = Color.grey;
            }
            else
            {
                turnTable[1 - playerTurn].GetComponent<SpriteRenderer>().color = Color.yellow;
                turnTable[playerTurn].GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }
        else if (turn == 8)
        {
            end = 2;
            timer.StopClock();
            turnTable[0].GetComponent<SpriteRenderer>().color = Color.grey;
            turnTable[1].GetComponent<SpriteRenderer>().color = Color.grey;
            if(playerTurn == 0)
            {
                PlayerPrefs.SetInt("Turn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Turn", 0);
            }
        }
        else
        {
            turn += 1;
            handling = false;
        }
    }

    // After Game is End, Load Scenes
    void SceneChanger()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (end == 1)
            {
                SceneManager.LoadScene("HomeScene");
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    bool Turn()
    {
        if (turn % 2 == playerTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
