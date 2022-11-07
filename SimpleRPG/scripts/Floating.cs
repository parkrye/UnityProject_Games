using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public int direction;
    public float speed;
    public float time;
    private float timer;
    private Vector3 position;

    private void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        position = gameObject.GetComponent<RectTransform>().position;
        if (timer < time)
        {
            switch (direction)
            {
                default:
                case 0:
                    gameObject.GetComponent<RectTransform>().position = new Vector3(position.x + speed, position.y, 0);
                    break;
                case 1:
                    gameObject.GetComponent<RectTransform>().position = new Vector3(position.x - speed, position.y, 0);
                    break;
                case 2:
                    gameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y + speed, 0);
                    break;
                case 3:
                    gameObject.GetComponent<RectTransform>().position = new Vector3(position.x, position.y - speed, 0);
                    break;
            }
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 _position)
    {
        position = _position;
    }
}
