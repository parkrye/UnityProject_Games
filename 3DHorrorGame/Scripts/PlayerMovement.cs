using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public new Light light;
    public ObjectManager objectManager;
    public Transform noisePoint;
    public Enemy enemy;
    public AudioSource footStep, breathing;
    public AudioManager audioManager;

    float x, z;
    Vector3 move;

    float speed = 5f;
    float runSpeed = 8f;
    float crawlSpeed = 3f;
    float gravity = -15f;
    float nowSpeed = 0f;

    float maxStamina = 10f;
    float stamina = 10f;
    float crawlHeight = 0.1f;
    float noiseTime = 1f;
    float noiseDist = 0f;
    float volume = 1f;

    bool lightOn = true;
    bool run = false;
    bool runable = true;
    bool crawl = false;
    bool moving = false;
    bool running = false;
    bool walking = false;
    bool onMenu = false;

    Vector3 velocity;

    private void Start()
    {
        breathing.Play();
        StartCoroutine(FootstepSound());
    }

    // Update is called once per frame
    void Update()
    {
        breathing.volume = volume * audioManager.GetVolume();
        if (!onMenu)
        {
            OpenMenu();
            FlashLight();
            Stamina();
            MoveMode();
            Crawl();
            CharacterMove();
        }
        else
        {
            QuitMenu();
        }
    }

    void MoveMode()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (runable)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                run = true;
                nowSpeed = runSpeed;
                crawl = false;
                if (walking && !running)
                {
                    StopCoroutine(WalkNoise());
                    StartCoroutine(RunNoise());
                    walking =  false;
                    running = true;
                }
                else if (!walking && !running)
                {
                    StartCoroutine(RunNoise());
                    running = true;
                }
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                run = false;
                crawl = true;
                nowSpeed = crawlSpeed;
                if (walking || running)
                {
                    StopCoroutine(RunNoise());
                    StopCoroutine(WalkNoise());
                    walking = false;
                    running = false;
                }
            }
            else
            {
                run = false;
                crawl = false;
                nowSpeed = speed;
                if (!walking && running)
                {
                    StopCoroutine(RunNoise());
                    StartCoroutine(WalkNoise());
                    walking = true;
                    running = false;
                }
                else if (!walking && !running)
                {
                    StartCoroutine(WalkNoise());
                    walking = true;
                }
            }
        }
        else
        {
            run = false;
            nowSpeed = crawlSpeed;
            if (walking || running)
            {
                StopCoroutine(RunNoise());
                StartCoroutine(WalkNoise());
                walking = false;
                running = false;
            }
        }
    }

    void FlashLight()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            lightOn = !lightOn;
            light.enabled = lightOn;
        }
    }

    void CharacterMove()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * nowSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Stamina()
    {
        if (runable)
        {
            if (run)
            {
                if (moving)
                {
                    if (stamina > 0f)
                    {
                        stamina -= Time.deltaTime;
                    }
                    else
                    {
                        runable = false;
                    }
                }
                else
                {
                    if (stamina < maxStamina)
                    {
                        stamina += Time.deltaTime;
                    }
                    else
                    {
                        stamina = maxStamina;
                    }
                }
            }
            else
            {
                if(stamina < maxStamina)
                {
                    stamina += Time.deltaTime;
                }
                else
                {
                    stamina = maxStamina;
                }
            }
        }
        else
        {
            if(stamina >= maxStamina / 2f)
            {
                runable = true;
            }
            else
            {
                stamina += Time.deltaTime * 0.5f;
            }
        }

        objectManager.StaminaModified(stamina, maxStamina);
    }

    void Crawl()
    {
        if (crawl)
        {
            if (transform.localScale.y > 0.5f)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y - crawlHeight, 1f);
            }
        }
        else
        {
            if (transform.localScale.y < 1f)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y + crawlHeight, 1f);
            }
        }
    }

    IEnumerator RunNoise()
    {
        while (run)
        {
            noiseDist = 20f;

            if (moving)
            {
                noisePoint.position = transform.position;
                enemy.HearNoise(noisePoint, noiseDist);
            }

            yield return new WaitForSeconds(noiseTime);
        }
    }

    IEnumerator WalkNoise()
    {
        while (!run && !crawl)
        {
            noiseDist = 5f;

            if (moving)
            {
                noisePoint.position = transform.position;
                enemy.HearNoise(noisePoint, noiseDist);
            }

            yield return new WaitForSeconds(noiseTime);
        }
    }

    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onMenu = true;
            objectManager.OpenMenu();
        }
    }

    void QuitMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onMenu = false;
            objectManager.QuitMenu();
        }
    }

    public void ControllOut()
    {
        onMenu = false;
    }

    IEnumerator FootstepSound()
    {
        while (true)
        {
            footStep.volume = volume * audioManager.GetVolume();
            if (moving)
            {
                footStep.Play();

                if (run)
                {
                    yield return new WaitForSeconds(0.2f);
                }
                else if (crawl)
                {
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            objectManager.GameOver();
        }
    }
}