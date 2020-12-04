using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueStruct
{
    public List<bool> preconditions; // the condition it needs to show this dialogue option
    public List<int> triggerIDs; // the cards the player needs to use to show this dialogue option
    [Header("neutral")]
    [Header("INQUIRE")]
    [TextArea]
    public List<string> dialogue_inquiring; // inquiring thread
    public List<GameObject> cards_inquiring; // cards this thread gives
    public bool inquiringCard_given; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_inquire; // characters that this approach unlocks
    public int relationshipChangeAmount_inquire; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_inquire;
    public List<GameObject> cardsLimitedTo_inquire;
    public List<GameObject> cardsToDestory_inquire;
    [Header("positive")]
    [TextArea]
    public List<string> dialogue_inquiring_p; // inquiring thread
    public List<GameObject> cards_inquiring_p; // cards this thread gives
    public bool inquiringCard_given_p; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_inquire_p; // characters that this approach unlocks
    public int relationshipChangeAmount_inquire_p; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_inquire_p;
    public List<GameObject> cardsLimitedTo_inquire_p;
    public List<GameObject> cardsToDestory_inquire_p;
    [Header("negative")]
    [TextArea]
    public List<string> dialogue_inquiring_n; // inquiring thread
    public List<GameObject> cards_inquiring_n; // cards this thread gives
    public bool inquiringCard_given_n; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_inquire_n; // characters that this approach unlocks
    public int relationshipChangeAmount_inquire_n; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_inquire_n;
    public List<GameObject> cardsLimitedTo_inquire_n;
    public List<GameObject> cardsToDestory_inquire_n;
    [Header("neutral")]
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
    [Header("positive")]
    [TextArea]
    public List<string> dialogue_threatened_p; // threatened thread
    public List<GameObject> cards_threatened_p; // cards this thread gives
    public bool threatenedCard_given_p; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_threat_p; // characters that this approach unlocks
    public int relationshipChangeAmount_threat_p; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_threat_p;
    public List<GameObject> cardsLimitedTo_threat_p;
    public List<GameObject> cardsToDestory_threat_p;
    [Header("negative")]
    [TextArea]
    public List<string> dialogue_threatened_n; // threatened thread
    public List<GameObject> cards_threatened_n; // cards this thread gives
    public bool threatenedCard_given_n; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_threat_n; // characters that this approach unlocks
    public int relationshipChangeAmount_threat_n; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_threat_n;
    public List<GameObject> cardsLimitedTo_threat_n;
    public List<GameObject> cardsToDestory_threat_n;
    [Header("neutral")]
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
    [Header("positive")]
    [TextArea]
    public List<string> dialogue_trading_p; // trading thread
    public List<GameObject> cards_trading_p; // cards this thread gives
    public bool tradingCard_given_p; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_trade_p; // characters that this approach unlocks
    public int relationshipChangeAmount_trading_p; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_trade_p;
    public List<GameObject> cardsLimitedTo_trade_p;
    public List<GameObject> cardsToDestory_trade_p;
    [Header("negative")]
    [TextArea]
    public List<string> dialogue_trading_n; // trading thread
    public List<GameObject> cards_trading_n; // cards this thread gives
    public bool tradingCard_given_n; // check if the card is already given
    public List<StateManagerScript.Charas> charasToUnlock_trade_n; // characters that this approach unlocks
    public int relationshipChangeAmount_trading_n; // relationship amount this approach changes
    public List<GameObject> cardsToLimit_trade_n;
    public List<GameObject> cardsLimitedTo_trade_n;
    public List<GameObject> cardsToDestory_trade_n;
}