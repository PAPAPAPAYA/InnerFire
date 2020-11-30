using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueStruct
{
    public List<bool> preconditions; // the condition it needs to show this dialogue option
    public List<int> triggerIDs; // the cards the player needs to use to show this dialogue option
    [Header("INQUIRE")]
    [TextArea]
    public List<string> dialogue_inquiring; // inquiring thread
    public List<GameObject> cards_inquiring; // cards this thread gives
    public bool inquiringCard_given; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_inquire; // characters that this approach unlocks
    public int relationshipChangeAmount_inquire; // relationship amount this approach changes
    [Header("THREAT")]
    [TextArea]
    public List<string> dialogue_threatened; // threatened thread
    public List<GameObject> cards_threatened; // cards this thread gives
    public bool threatenedCard_given; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_threat; // characters that this approach unlocks
    public int relationshipChangeAmount_threat; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_threat;
    public List<GameObject> cardsLimitedTo_threat;
    public List<GameObject> cardsToDestory_threat;
    [Header("TRADE")]
    [TextArea]
    public List<string> dialogue_trading; // trading thread
    public List<GameObject> cards_trading; // cards this thread gives
    public bool tradingCard_given; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_trade; // characters that this approach unlocks
    public int relationshipChangeAmount_trading; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_trade;
    public List<GameObject> cardsLimitedTo_trade;
    public List<GameObject> cardsToDestory_trade;
}