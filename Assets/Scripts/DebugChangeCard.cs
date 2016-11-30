using UnityEngine;
using System.Collections;

public class DebugChangeCard : MonoBehaviour {

    private CardModel cardModel;
    private int cardIndex = 0;

    public GameObject card;


    // Use this for initialization
    void Awake () {
        cardModel = card.GetComponent<CardModel>();
	}
	
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            cardIndex = Random.Range(0, 52);
            cardModel.cardIndex = cardIndex;
            cardModel.ToggleFace(true);


            /*
            if (cardIndex >= cardModel.faces.Length)
            {
                cardIndex = 0;
                cardModel.ToggleFace(false);
            }
            else
            {
                cardModel.cardIndex = cardIndex;
                cardModel.ToggleFace(true);
                cardIndex++;
            }
            */
        }
    }
}
