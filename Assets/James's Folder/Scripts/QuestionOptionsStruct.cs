using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestionOptionsStruct
{
    // one question can lead to a list of dialogues, gives card(s) after dialogue, change relationship (if viable)
    public string question;
    [TextArea]
    public List<string> dialogues;
    public List<GameObject> cardsItGives;
    public int relationshipChangeAmount;
    public bool shown;
}