using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;

public class BirdMove : MonoBehaviour
{
    private static BirdMove player = null;

    void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static BirdMove Player
    {
        get
        {
            if (player == null)
            {
                return null;
            }
            return player;
        }
    }

    public GameObject[] uis;
    public Slider leftWingSlider, rightWingSlider, tailSlider;
    public Toggle leftWingToggle, rightWingToggle, groundToggle, lookbackTogle;

    public Transform tail, look;
    public Transform[] wings = new Transform[2], shoulders = new Transform[2];    // 오른쪽, 왼쪽

    public Rigidbody rigidBody;

    int headUp, headRight, headDirection;
    float tailUp, shoulderL, shoulderR;

    public float power, turnSpeed, walkSpeed, jumpPower;

    bool ground, flipWings, useCursor;
    int flutterCounter;

    public Transform spawner;
    public GameObject eggPrefab;
    public Text[] texts;

    public AudioSource weep, flap;

    bool clear;

    void Start()
    {
        StartCoroutine(Loading());
    }

    /// <summary>
    /// 로딩 시간 대기
    /// </summary>
    /// <returns></returns>
    IEnumerator Loading()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        useCursor = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uis[0].SetActive(false);
        uis[1].SetActive(true);
        yield return new WaitForSeconds(3);
        flipWings = true;
        useCursor = false;
        UseCursor(useCursor);
        uis[1].SetActive(false);
        StartCoroutine(Flutting());
    }

    // Update is called once per frame
    void Update()
    {
        if (!useCursor)
        {
            OnGround();
            Walk();
            HeadMove();
            TailMove();
            ShoulderMove();
            WingMove();
            SpawnEggs();
            Weep();
        }
    }

    /// <summary>
    /// 방향키로 머리 회전. 시야 조정
    /// </summary>
    void HeadMove()
    {
        // 후방 보기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            headDirection = 180;
            lookbackTogle.isOn = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            headDirection = 0;
            lookbackTogle.isOn = false;
        }

        // 상하 회전
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (headUp > -45)  headUp += -1;
            else headUp = -45;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (headUp < 45) headUp += 1;
            else  headUp = 45;
        }
        
        // 좌우 회전
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (headRight > -45) headRight += -1;
            else headRight = -45;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (headRight < 45) headRight += 1;
            else headRight = 45;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            headUp = 0;
            headRight = 0;
        }

        look.localEulerAngles = new Vector3(headUp, headDirection + headRight, 0f);
    }

    /// <summary>
    /// 키패드 숫자키로 꼬리 회전, 방향 조정
    /// </summary>
    void TailMove()
    {
        // 몸통 회전
        if (Input.GetKey(KeyCode.Keypad4)) transform.Rotate(transform.up * Time.deltaTime * -turnSpeed);
        else if (Input.GetKey(KeyCode.Keypad6)) transform.Rotate(transform.up * Time.deltaTime * turnSpeed);

        // 꼬리 회전
        if (Input.GetKey(KeyCode.Keypad8))
        {
            if(tailUp < 30f) tailUp += 1f;
            else tailUp = 30f;
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            if (tailUp > -30f) tailUp += -1f;
            else tailUp = -30f;
        }
        else
        {
            if (!ground)
            {
                float modifiyer = Random.Range(0f, 0.005f);
                if (tailUp > 0f) tailUp += -modifiyer;
                else if (tailUp < 0f) tailUp += modifiyer;
                else
                {
                    if (Random.Range(0, 2) == 0) tailUp += -modifiyer;
                    else tailUp += modifiyer;
                }
            }
        }

        // 각도 리셋
        if (Input.GetKey(KeyCode.Keypad5)) tailUp = 0f;

        tail.localEulerAngles = new Vector3(tailUp, 0f, 0f);
        tailSlider.value = (tailUp + 30f) / 60f;
    }

    /// <summary>
    /// Q,Z,E,C, S로 날개 각도 조정
    /// </summary>
    void ShoulderMove()
    {
        // 오른쪽 날개
        if(Input.GetKey(KeyCode.E))
        {
            if (shoulderR < 30f)
            {
                shoulderR += 0.5f;
                shoulders[0].Rotate(0.5f, 0f, 0f);
            }
            else
            {
                shoulderR = 30f;
                shoulders[0].localEulerAngles = new Vector3(shoulderR, 0f, 0f);
            }
        }
        else if(Input.GetKey(KeyCode.C))
        {
            if (shoulderR > -30f)
            {
                shoulderR += -0.5f;
                shoulders[0].Rotate(-0.5f, 0f, 0f);
            }
            else
            {
                shoulderR = -30f;
                shoulders[0].localEulerAngles = new Vector3(shoulderR, 0f, 0f);
            }
        }
        else
        {
            if (!ground)
            {
                float modifiyer = Random.Range(0f, 0.005f);
                if (shoulderR > 0f)
                {
                    shoulderR += -modifiyer;
                    shoulders[0].Rotate(-modifiyer, 0f, 0f);
                }
                else if (shoulderR < 0f)
                {
                    shoulderR += modifiyer;
                    shoulders[0].Rotate(modifiyer, 0f, 0f);
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        shoulderR += -modifiyer;
                        shoulders[0].Rotate(-modifiyer, 0f, 0f);
                    }
                    else
                    {
                        shoulderR += modifiyer;
                        shoulders[0].Rotate(modifiyer, 0f, 0f);
                    }
                }
            }
        }

        // 왼쪽 날개
        if (Input.GetKey(KeyCode.Q))
        {
            if (shoulderL > -30f)
            {
                shoulderL += -0.5f;
                shoulders[1].Rotate(-0.5f, 0f, 0f);
            }
            else
            {
                shoulderL = -30f;
                shoulders[1].localEulerAngles = new Vector3(shoulderL, 180f, 0f);
            }
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            if (shoulderL < 30f)
            {
                shoulderL += 0.5f;
                shoulders[1].Rotate(0.5f, 0f, 0f);
            }
            else
            {
                shoulderL = 30f;
                shoulders[1].localEulerAngles = new Vector3(shoulderL, 180f, 0f);
            }
        }
        else
        {
            if (!ground)
            {
                float modifiyer = Random.Range(0f, 0.005f);
                if (shoulderL < 0f)
                {
                    shoulderL += modifiyer;
                    shoulders[1].Rotate(modifiyer, 0f, 0f);
                }
                else if (shoulderL > 0f)
                {
                    shoulderL += -modifiyer;
                    shoulders[1].Rotate(-modifiyer, 0f, 0f);
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        shoulderL += modifiyer;
                        shoulders[1].Rotate(modifiyer, 0f, 0f);
                    }
                    else
                    {
                        shoulderL += -modifiyer;
                        shoulders[1].Rotate(-modifiyer, 0f, 0f);
                    }
                }
            }
        }

        // 각도 리셋
        if (Input.GetKey(KeyCode.S))
        {
            shoulderR = 0f;
            shoulders[0].localEulerAngles = new Vector3(0f, 0f, 0f);
            shoulderL = 0f;
            shoulders[1].localEulerAngles = new Vector3(0f, 180f, 0f);
        }

        rightWingSlider.value = (shoulderR + 30f) / 60f;
        leftWingSlider.value = (-shoulderL + 30f) / 60f;
    }

    /// <summary>
    /// A,D로 비행
    /// </summary>
    void WingMove()
    {

        // 오른쪽 날개
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 totalVector = Vector3.zero;
            rightWingToggle.isOn = true;
            flutterCounter += 1;
            shoulders[0].Rotate(0f, 0f, -30f);
            flap.PlayOneShot(flap.clip);

            // 기본 이동
            Vector3 move = -transform.right + transform.up;

            // 꼬리 방향
            move += transform.up * (1f + tailUp / 30f);

            // 날개 각도
            move += transform.forward * Mathf.Lerp(-1f, 1f, (shoulderR + 30f) / 60f);

            totalVector += move;
            rigidBody.AddForce(totalVector * power, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Vector3 totalVector = Vector3.zero;
            rightWingToggle.isOn = false;
            shoulders[0].Rotate(0f, 0f, 30f);

            // 기본 이동
            Vector3 move = -transform.right - transform.up;

            // 날개 각도
            move += transform.forward * -Mathf.Lerp(-1f, 1f, (shoulderR + 30f) / 60f);

            totalVector += move / 2;
            rigidBody.AddForce(totalVector * power, ForceMode.Impulse);
        }

        // 왼쪽 날개
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 totalVector = Vector3.zero;
            leftWingToggle.isOn = true;
            flutterCounter += 1;
            shoulders[1].Rotate(0f, 0f, -30f);
            flap.PlayOneShot(flap.clip);

            // 기본 이동
            Vector3 move = transform.right + transform.up;

            // 꼬리 방향
            move += transform.up * (1f + tailUp / 30f);

            // 날개 각도
            move += transform.forward * Mathf.Lerp(-1f, 1f, (-shoulderL + 30f) / 60f);

            totalVector += move;
            rigidBody.AddForce(totalVector * power, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Vector3 totalVector = Vector3.zero;
            leftWingToggle.isOn = false;
            shoulders[1].Rotate(0f, 0f, 30f);

            // 기본 이동
            Vector3 move = transform.right - transform.up;

            // 날개 각도
            move += transform.forward * -Mathf.Lerp(-1f, 1f, (-shoulderL + 30f) / 60f);

            totalVector += move / 2;
            rigidBody.AddForce(totalVector * power, ForceMode.Impulse);
        }
    }
    
    /// <summary>
    /// W, X로 걷기
    /// </summary>
    void Walk()
    {
        if (ground && flutterCounter < 2)
        {
            if (!flipWings)
            {
                flipWings = true;
                wings[0].localPosition = new Vector3(0f, 0f, 0f);
                wings[1].localPosition = new Vector3(0f, 0f, 0f);
                wings[0].localScale = new Vector3(1f, 0.2f, 1f);
                wings[1].localScale = new Vector3(1f, 0.2f, 1f);
            }

            if (Input.GetKey(KeyCode.W)) rigidBody.AddForce(transform.forward * walkSpeed, ForceMode.Force);
            else if (Input.GetKey(KeyCode.X)) rigidBody.AddForce(-transform.forward * walkSpeed, ForceMode.Force);

            if (Input.GetKeyDown(KeyCode.Space)) rigidBody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
        else
        {
            if (flipWings)
            {
                flipWings = false;
                wings[0].localPosition = new Vector3(0.5f, 0f, 0f);
                wings[1].localPosition = new Vector3(0.5f, 0f, 0f);
                wings[0].localScale = new Vector3(0.2f, 1f, 1f);
                wings[1].localScale = new Vector3(0.2f, 1f, 1f);
            }
        }
    }

    /// <summary>
    /// 현재 땅에 서있는지 확인하는 함수
    /// </summary>
    void OnGround()
    {
        Debug.DrawRay(transform.position, -transform.up * 1.2f, Color.red);
        if (Physics.Raycast(transform.position, -transform.up, 1.1f)) ground = true;
        else ground = false;
        groundToggle.isOn = ground;
    }

    /// <summary>
    /// Alt로 울기
    /// </summary>
    void Weep()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt)) weep.Play();
    }

    IEnumerator Flutting()
    {
        flutterCounter = 0;
        while (true)
        {
            if (flutterCounter > 5) flutterCounter = 5;
            if (flutterCounter > 0) flutterCounter -= 1;
            yield return new WaitForSeconds(0.5f);
        }
    }
 
    void SpawnEggs()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) Instantiate(eggPrefab, spawner);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForStage());
    }

    IEnumerator WaitForStage()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity= Vector3.zero;
        useCursor = true;
        if (!clear) transform.position = new Vector3(0, 2, 0);
        else transform.position = new Vector3(0, 2, -40);
        transform.eulerAngles = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1.5f);
        useCursor = false;
        uis[0].SetActive(true);
        uis[1].SetActive(false);
        string stage = SceneManager.GetActiveScene().name.Substring(5, 1);
        if(stage != "c")
        {
            int stageNum = int.Parse(stage);
            uis[2].SetActive(true);
            uis[2].GetComponentsInChildren<Text>()[0].text = ScriptsManager.Instance.GetStageScript(0, stageNum);
            uis[2].GetComponentsInChildren<Text>()[1].text = ScriptsManager.Instance.GetStageScript(1, stageNum);
            for(int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(1.5f);
                uis[2].GetComponentsInChildren<Text>()[0].color = new Color(0.2f, 0.2f, 0.2f, (10 - i)/10);
                uis[2].GetComponentsInChildren<Text>()[1].color = new Color(0.2f, 0.2f, 0.2f, (10 - i)/10);
            }
            uis[2].SetActive(false);
        }
    }

    public void ClearGame()
    {
        clear = !clear;
    }

    public void Translate()
    {
        // 플레이정보 UI
        texts[0].text = ScriptsManager.Instance.GetInGameUIScript(0);
        texts[1].text = ScriptsManager.Instance.GetInGameUIScript(1);
        texts[2].text = ScriptsManager.Instance.GetInGameUIScript(2);
        texts[3].text = ScriptsManager.Instance.GetInGameUIScript(3);
        texts[4].text = ScriptsManager.Instance.GetInGameUIScript(4);
    }

    public void UseCursor(bool use)
    {
        useCursor = use;
        if (useCursor)
        {
            Time.timeScale = 0f;
            Cursor.visible = useCursor;
            Cursor.lockState = CursorLockMode.None;
            uis[0].SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = useCursor;
            Cursor.lockState = CursorLockMode.Locked;
            uis[0].SetActive(true);
        }
    }

    public void ShowWaitImage()
    {
        uis[0].SetActive(false);
        uis[1].SetActive(true);
    }
}
