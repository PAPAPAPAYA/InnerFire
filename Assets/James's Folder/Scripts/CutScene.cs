using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutScene : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    [TextArea]
    public List<string> lines;
	private int lineIndex;
	public int cutsceneIndex;

	private void Update()
	{
		if (lineIndex < lines.Count)
		{
			subtitle.text = lines[lineIndex];
		}
	}

	private void OnMouseDown()
	{
		lineIndex++; // next line
		if (lineIndex >= lines.Count)
		{
			if (cutsceneIndex == 0 || // office
				cutsceneIndex == 1 || // great escape 1
				cutsceneIndex == 2 || // great escape 2
				cutsceneIndex == 5) // fire
			{
				CutSceneManager.me.blackCover.GetComponent<Transition>().FadeOut(cutsceneIndex + 1, gameObject);
				//CutSceneManager.me.ShowCutscene(cutsceneIndex+1); // show next cutscene
			}
			else if (cutsceneIndex == 6) // newspaper
			{
				// fade out then enter day 2
				CutSceneManager.me.blackCover.GetComponent<Transition>().FadeOut(10, gameObject);
			}
			else // handshake or enter village or fire
			{
				CutSceneManager.me.blackCover.GetComponent<Transition>().FadeOut(9, gameObject);
			}
			// deactivate self
			//gameObject.SetActive(false);
		}
	}
}
