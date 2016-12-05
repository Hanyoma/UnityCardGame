using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class GameController : NetworkBehaviour
{
    private List<Card> deck;
    private List<Card> discard;

    private CardModel cardModel;
    private GameObject card;
    public GameObject card_prefab;
    private bool created = false;

    void Awake()
    {
        deck = new List<Card>();

        // initialize deck
        foreach(Card.Suit s in Enum.GetValues(typeof(Card.Suit)))
        {
            for(int i = 1; i < 14; ++i)
            {
                deck.Add(new Card(s, i));
            }
        }
        Shuffle(deck);
        discard = new List<Card>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!created)
        {
            CmdMakeCard();
            created = true;
        }
        else
        {
            var x = Input.GetAxis("Horizontal") * 0.1f;
            var z = Input.GetAxis("Vertical") * 0.1f;
            card.transform.Translate(x,0,z);
        }
    }

    [Command]
    void CmdMakeCard()
    {
        print("cmdmakecard");
        // this [Command] is run on the server
        card = Instantiate(card_prefab) as GameObject;
        cardModel = card.GetComponent<CardModel>();
        NetworkServer.Spawn(card);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            print("hit me hit");
            if (deck.Count == 0) return;
            discard.Add(cardModel.card);
            cardModel.card = new Card(deck[0]);
            print(cardModel);
            deck.RemoveAt(0);
            cardModel.ToggleFace(true);
        }
    }

    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
