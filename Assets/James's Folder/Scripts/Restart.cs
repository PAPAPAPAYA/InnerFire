using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
	{
		SoundManager.me.PlayClick();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
