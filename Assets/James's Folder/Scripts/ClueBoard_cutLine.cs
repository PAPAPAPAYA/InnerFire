using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoard_cutLine : MonoBehaviour
{
	public GameObject exitCutModeButton;
	public void CutLineMode()
	{
		SoundManager.me.PlayClick();
		ClueBoard.me.cutMode = true;
		foreach (var line in ClueBoard.me.lines)
		{
			LineScript ls = line.GetComponent<LineScript>();
			CapsuleCollider capsule = line.AddComponent<CapsuleCollider>();
			capsule.radius = 2.5f;
			capsule.center = Vector3.zero;
			capsule.direction = 2;
			capsule.transform.position = ls.aT.position + (ls.bT.position - ls.aT.position) / 2;
			capsule.transform.LookAt(ls.aT.position);
			capsule.height = (ls.bT.position - ls.aT.position).magnitude/2.5f;
		}
		exitCutModeButton.SetActive(true);
		gameObject.SetActive(false);
	}
}
