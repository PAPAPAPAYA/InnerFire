using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager me;
    public AudioSource click;
    public AudioSource cutLine;
    public AudioSource pin;
    public AudioSource bgm1;
    public AudioSource bgm2;

	private void Start()
	{
        me = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
            //PlayClick();
        }
	}

	public void PlayClick()
	{
        click.PlayOneShot(click.clip); // OneShot CANNOT be interrupted
    }

    public void PlayPin()
    {
        click.PlayOneShot(pin.clip); // OneShot CANNOT be interrupted
    }

    public void PlayCut()
	{
        click.PlayOneShot(cutLine.clip); // OneShot CANNOT be interrupted
    }

    public void PlayBGM1()
	{
        // loop
        if (bgm1.isPlaying)
        {
            bgm1.Stop();
        }
        else if (!bgm1.isPlaying)
        {
            bgm1.Play(); // Play will loop if "loop = true" alreadyx
        }
    }

    public void PlayBGM2()
    {
        // loop
        if (bgm2.isPlaying)
        {
            bgm2.Stop();
        }
        else if (!bgm2.isPlaying)
        {
            bgm2.Play(); // Play will loop if "loop = true" alreadyx
        }
    }

    public void StopBGM1()
	{

	}

}
