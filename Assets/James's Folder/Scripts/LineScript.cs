using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public Transform aT;
    public Transform bT;
    private LineRenderer lr;
    //public GameObject whoseMotiveItIs;
    public GameObject cardAttached;
    public GameObject memoAttached;
    public bool released = false;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (aT != null)
		{
            lr.SetPosition(0, new Vector3(aT.position.x, aT.position.y, aT.position.z + 0.1f));
            lr.SetPosition(1, new Vector3(bT.position.x, bT.position.y, bT.position.z + 0.1f));
        }
        if (released)
		{
            if (cardAttached == null)
			{
                ClueBoard.me.lines.Remove(gameObject);
                Destroy(gameObject);
			}
			else
			{
			}
		}
    }

	private void OnMouseDown()
	{
        SoundManager.me.PlayCut();
        ClueBoard.me.lines.Remove(gameObject);
        memoAttached.GetComponent<CreateLine>().lines.Remove(gameObject);
        memoAttached.GetComponent<CreateLine>().UpdateMotiveText();
        Destroy(gameObject);
	}
}
