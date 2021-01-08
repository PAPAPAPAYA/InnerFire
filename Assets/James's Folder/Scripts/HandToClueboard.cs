using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandToClueboard : MonoBehaviour
{
	public bool addMeToClueBoard;
	public bool imAMotiveCard;
	public bool imAFireCard;
	[TextArea]
	public string motiveText;
	[TextArea]
	public string fireText;
	public StateManagerScript.Charas whoToConnect;
}
