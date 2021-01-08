using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardNotification : MonoBehaviour
{
    public float existTime;
    private float timer;

	private void Start()
	{
		timer = existTime;
	}

	// Update is called once per frame
	void Update()
    {
        if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			timer = existTime;
			gameObject.SetActive(false);
		}
    }
}
