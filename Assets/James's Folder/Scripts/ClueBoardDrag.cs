using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoardDrag : MonoBehaviour
{
	private Vector3 mOffset;
	private float mZCoord;
	public bool canBeDragged = false;
	

	private void OnMouseDown()
	{
		mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		//mZCoord = Camera.main.WorldToScreenPoint(cam.transform.position).z;
		//mOffset = cam.transform.position - GetMouseWorldPos();
		mOffset = gameObject.transform.position - GetMouseWorldPos();
	}

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord;
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseDrag()
	{
		if (!ClueBoard.me.cutMode)
		{
			gameObject.transform.position = GetMouseWorldPos() + mOffset;
		}
	}

	
}
