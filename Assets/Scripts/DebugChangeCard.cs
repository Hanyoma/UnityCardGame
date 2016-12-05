using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DebugChangeCard : NetworkBehaviour {

    private CardModel cardModel;

    [SyncVar(hook="IndexChanged")]
    private int cardIndex = 0;

    public GameObject card;


    // Use this for initialization
    void Awake () {
        print("I exist");
	}
	
    void OnGUI()
    {

        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            if (!isServer)
            {
                return;
            }
            print("PRESSED A BUTTON");
            cardIndex = Random.Range(0, 52);

        }
    }

    void IndexChanged(int index)
    {
        card = GameObject.FindWithTag("Player");
        cardModel = card.GetComponent<CardModel>();
        cardModel.cardIndex = index;
        cardModel.ToggleFace(true);

    }
}
