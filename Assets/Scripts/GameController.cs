﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class GameController : NetworkBehaviour
{
    public GameObject cardPrefab;
    private Card.Robot robot = Card.Robot.Null;
    private List<GameObject> hand = new List<GameObject>();
    private GameObject opponentCard = null;
    private GameObject myCard = null;
    private GameObject opponentRobot = null;
    private GameObject myRobot = null;

    public bool disableCards;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += makeDeck;
        transform.Translate(new Vector3(-7, 0, 0));
        opponentRobot.transform.position = new Vector3(7, 0, 0);
        myRobot.transform.position = new Vector3(-7, 0, 0);
    }
    
    public override void OnStartLocalPlayer()
    {
        GameObject.Find("Robot_Select").GetComponent<Button>().onClick.AddListener(() => onRobot_SelectClick());
    }

    void makeDeck(Scene previousScene, Scene newScene)
    {
        if (!isLocalPlayer) return;
        List<string> names = new List<string>();
        switch (robot)
        {
            case Card.Robot.TheOriginal:
                names.Add("BLINK");
                names.Add("BUCKLE DOWN");
                names.Add("COUNTER");
                names.Add("DODGE");
                names.Add("DUCK");
                names.Add("FLAMETHROWER");
                names.Add("LASER");
                names.Add("LUNGE");
                names.Add("NAIL GUN");
                names.Add("SPRINT");
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("The Original - Character Card", typeof(Sprite)) as Sprite;
                break;
            case Card.Robot.DiscoFever:
                names.Add("DISCO BLAST");
                names.Add("ELECTRIC SHUFFLE");
                names.Add("JUMP");
                names.Add("MOONWALK");
                names.Add("OVERDRIVE");
                names.Add("SALSA SMACKDOWN");
                names.Add("SIDE STEP");
                names.Add("STUN STEP");
                names.Add("TRIPLE STEP PART ONE");
                names.Add("TRIPLE STEP PART TWO");
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Disco Fever - Character Card", typeof(Sprite)) as Sprite;
                break;

        }
        Shuffle(names);
        foreach(string name in names)
        {
            print(name);
        }
        for (int i = 0; i < 4; ++i)
        {
            GameObject cd = Instantiate(cardPrefab, new Vector3(-7 + i*2, 0, 1), Quaternion.identity) as GameObject;
            CardModel cm = cd.GetComponent<CardModel>();
            cm.card = new Card(robot, names[i]);

            GameObject cd_o = Instantiate(cardPrefab, new Vector3(7 - i*2, 0, 1), Quaternion.identity) as GameObject;
            cd_o.GetComponent<CardModel>().setOpponent();
        }
        for(int i = 4; i < 10; ++i)
        {
            GameObject cd = Instantiate(cardPrefab, new Vector3(-13 + i*2, -3, 1), Quaternion.identity) as GameObject;
            CardModel cm = cd.GetComponent<CardModel>();
            cm.card = new Card(robot, names[i]);
            cm.setGameController(this);
            cm.ToggleFace(true);
            hand.Add(cd);
            cd.gameObject.AddComponent(typeof(BoxCollider2D));
        }
    }

    public void onRobot_SelectClick()
    {
        if (!isLocalPlayer) return;
        print("is local player");
        
        robot = (Card.Robot)GameObject.Find("Robot_Dropdown").GetComponent<Dropdown>().value;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void CardChosen(CardModel c)
    {
        if (!isLocalPlayer) return;

        
        // c was chosen, send to server and disable all inputs
        print("cardchosen called local player");
        foreach(GameObject cm in hand)
        {
            cm.GetComponent<BoxCollider2D>().enabled = false;
        }
        GameObject c_gm = c.gameObject;
        hand.Remove(c_gm);
        myCard = c_gm;
        c_gm.transform.position = new Vector3(-1, 3, 1);

        CmdCardChosen(c.card, GetComponent<NetworkIdentity>().connectionToClient.connectionId);
    }

    [Command]
    private void CmdCardChosen(Card card, int connectionId)
    {
        print("CmdCardChosen");
        RpcCardChosen(card, connectionId);
        // if both people chose their card
        RpcExecuteTurn();
    }

    [ClientRpc]
    void RpcCardChosen(Card c, int origin)
    {
        print("RpcCardChosen");
        if(GetComponent<NetworkIdentity>().connectionToClient.connectionId != origin)
        {
            print("not the originator");
            opponentCard = Instantiate(cardPrefab, new Vector3(1, 3, 1), Quaternion.identity) as GameObject;
            CardModel cm = opponentCard.GetComponent<CardModel>();
            cm.card = c;
            cm.setGameController(this);
            cm.ToggleFace(true);
        }
    }

    [ClientRpc]
    void RpcExecuteTurn()
    {
        int myPos = (int)myRobot.transform.position.x;
        int enemyPos = (int)opponentRobot.transform.position.x;
        Card mCard = myCard.GetComponent<Card>();
        Card eCard = opponentCard.GetComponent<Card>();

        move(ref myPos, ref enemyPos, mCard.backstep, eCard.backstep, mCard, eCard);
        move(ref myPos, ref enemyPos, mCard.move, eCard.move, mCard, eCard);
        ranged(ref myPos, ref enemyPos, mCard, eCard);
        move(ref myPos, ref enemyPos, mCard.blink, eCard.blink, mCard, eCard);

        // do game logic
        // reenable hand cards
    }

    void move(ref int myPos, ref int enemyPos, int myMoves, int enemyMoves, Card mCard, Card oCard)
    {
        if(myPos == enemyPos)
        {
            if(fight(mCard, oCard))
                return;
        }
        while(myMoves != 0 || enemyMoves != 0)
        {
            if (myPos == enemyPos)
            {
                if(fight(mCard, oCard))
                {
                    myPos = myPos - 1;
                    enemyPos = enemyPos - 1;
                    myMoves = 0;
                    enemyMoves = 0;
                }
            }
            else if (myPos == 7)
            {
                // I win
            }
            else if(enemyPos == -7)
            {
                // They win
            }
            else if(Math.Abs(myPos-enemyPos) == 1)
            {
                if(fight(mCard, oCard))
                {
                    myMoves = 0;
                    enemyMoves = 0;
                }
            }
            else
            {
                if(myMoves > 0)
                {
                    myPos++;
                    myMoves--;
                }
                else if(myMoves < 0)
                {
                    myPos--;
                    myMoves++;
                }
                if(enemyMoves > 0)
                {
                    enemyPos++;
                    enemyMoves--;
                }
                else if(enemyMoves < 0)
                {
                    enemyPos--;
                    enemyMoves++;
                }
            }
        }

    }

    // If true, bounceback
    // If false, pass
    bool fight(Card mCard, Card oCard)
    {
        if(mCard.perfectMelee || oCard.perfectMelee)
        {
            if(mCard.perfectMelee && oCard.perfectMelee)
            {
                return true;
            }
            else
            {
                if (mCard.perfectMelee)
                {
                    // They lose
                }
                else
                {
                    // I lose
                }
            }
            return false;
        }
        else if(mCard.melee || oCard.melee)
        {
            if (mCard.melee)
            {
                if (!oCard.evadeMelee && !oCard.melee)
                {
                    // I win
                }
            }
            else
            {
                if(!mCard.evadeMelee && !mCard.melee)
                {
                    // I lose
                }
            }
            if(mCard.evadeMelee || oCard.evadeMelee)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    void ranged(ref int myPos, ref int enemyPos, Card mCard, Card oCard) 
    {
        int myDistance = Math.Abs(myPos) + Math.Abs(enemyPos);
        bool iCanHit = false;
        bool enemyCanHit = false; 

        if (myPos > enemyPos)
            myDistance = -myDistance;

        if(mCard.ranged || oCard.ranged)
        {
            if (mCard.ranged && !oCard.evadeRanged)
            {
                iCanHit = myDistance > mCard.rangedCloseDist && myDistance < mCard.rangedFarDist;
            }
            if(oCard.ranged && !mCard.evadeRanged)
            {
                enemyCanHit = myDistance > oCard.rangedCloseDist && myDistance < oCard.rangedFarDist;
            }

            if(iCanHit || enemyCanHit)
            {
                if(iCanHit && enemyCanHit)
                {
                    //No one died
                    return;
                }
                else if (iCanHit)
                {
                    // I win
                }
                else
                {
                    // I lose
                }
            }
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
