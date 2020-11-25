using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionOptionsManager : MonoBehaviour
{
    static public QuestionOptionsManager me;
    public List<GameObject> questionButtons;
	public List<TextMeshProUGUI> buttonTexts;
	//public int questionChosen;

	private void Awake()
	{
		me = this;
	}

	public void HideQuestionButtons()
	{
		foreach (var ugui in buttonTexts)
		{
			ugui.text = "";
		}
		foreach (var button in questionButtons)
		{
			button.SetActive(false);
		}
	}
}
