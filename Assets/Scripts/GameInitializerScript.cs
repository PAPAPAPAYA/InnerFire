using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> initialCards;

    void Awake()
    {
        // refresh player card
        playerPrefab.GetComponent<PlayerScript>().handPrefabs.Clear(); // clear old list
		foreach (GameObject card in initialCards)
		{
            playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(card); // make new cards
        }
    }
}
