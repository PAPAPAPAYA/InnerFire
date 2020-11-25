using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueStruct
{
    public List<bool> preconditions; // the condition it needs to show this dialogue option
    public List<int> triggerIDs; // the cards the player needs to use to show this dialogue option
    [TextArea]
    public List<string> dialogue; // dialogue before using any cards
    [TextArea]
    public List<string> dialogue_inquiring; // inquiring thread
    public List<GameObject> cards_inquiring; // cards this thread gives
    public bool inquiringCard_given; // check if the card is already given
    [TextArea]
    public List<string> dialogue_threatened; // threatened thread
    public List<GameObject> cards_threatened; // cards this thread gives
    public bool threatenedCard_given; // check if the card is already given
    [TextArea]
    public List<string> dialogue_trading; // trading thread
    public List<GameObject> cards_trading; // cards this thread gives
    public bool tradingCard_given; // check if the card is already given
}