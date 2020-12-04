using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestionOptionsStruct
{
    // one question can lead to a list of dialogues, gives card(s) after dialogue, change relationship (if viable)
    public List<GameObject> preconditionCharas;
    public string question;
    public bool shown;
    public bool precondition_met;
    [Header("neutral")]
    [TextArea]
    public List<string> dialogues;
    public List<GameObject> cardsItGives;
    public int relationshipChangeAmount;
    public List<StateManagerScript.Charas> charasToUnlock;
    public List<GameObject> cardsItLimits;
    public List<GameObject> cardsLimitedTo;
    public List<GameObject> cardsItDestroys;
    [Header("positive")]
    [TextArea]
    public List<string> dialogues_p;
    public List<GameObject> cardsItGives_p;
    public int relationshipChangeAmount_p;
    public List<StateManagerScript.Charas> charasToUnlock_p;
    public List<GameObject> cardsItLimits_p;
    public List<GameObject> cardsLimitedTo_p;
    public List<GameObject> cardsItDestroys_p;
    [Header("negative")]
    [TextArea]
    public List<string> dialogues_n;
    public List<GameObject> cardsItGives_n;
    public int relationshipChangeAmount_n;
    public List<StateManagerScript.Charas> charasToUnlock_n;
    public List<GameObject> cardsItLimits_n;
    public List<GameObject> cardsLimitedTo_n;
    public List<GameObject> cardsItDestroys_n;
}