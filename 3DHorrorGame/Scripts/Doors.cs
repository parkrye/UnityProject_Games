using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
	public Animator animator;
	public bool open;
	public Transform player;
	public new AudioSource audio;
	public AudioManager audioManager;
	public float volume = 1f;

	void Start()
	{
		audio = GetComponent<AudioSource>();
		if (!audio)
		{
			Destroy(this);
		}
        else
		{
			audio.playOnAwake = false;
			open = false;
			player = GameObject.FindGameObjectWithTag("Player").transform;
			if (!player)
			{
				Destroy(this);
			}
            else
			{
				animator = GetComponent<Animator>();
				if (!animator)
				{
					Destroy(this);
				}
                else
				{
					audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
					if (!audioManager)
					{
						Destroy(this);
					}
				}
			}
		}
	}

	void OnMouseOver()
	{
		if (player)
		{
			float dist = Vector3.Distance(player.position, transform.position);
			if (dist < 5)
			{
				if (!open)
				{
					if (Input.GetMouseButtonDown(0))
					{
						StartCoroutine(opening());
					}
				}
				else
				{
					if (Input.GetMouseButtonDown(0))
					{
						StartCoroutine(closing());
					}
				}
			}
		}

	}

	IEnumerator opening()
	{
		audio.volume = volume * audioManager.GetVolume();
		audio.Play();
		animator.Play("Open");
		yield return new WaitForSeconds(.5f);
	}

	IEnumerator closing()
	{
		audio.volume = volume * audioManager.GetVolume();
		audio.Play();
		animator.Play("Close");
		yield return new WaitForSeconds(.5f);
	}

	void OnAnimationEnd()
	{
		open = !open;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (!open)
			{
				StartCoroutine(opening());
			}
        }
    }
}
