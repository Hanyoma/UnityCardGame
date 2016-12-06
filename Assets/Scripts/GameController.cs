using UnityEngine;
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
    private GameObject myRobot = null;
    private GameObject oppRobot = null;

    private const string OPPTAG = "oppCard";
    private const string MYTAG = "myCard";

    public bool disableCards;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += makeDeck;
        transform.Translate(new Vector3(-7, 0, 0));
    }
    
    public override void OnStartLocalPlayer()
    {
        GameObject.Find("Robot_Select").GetComponent<Button>().onClick.AddListener(() => onRobot_SelectClick());
        myRobot = 
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
        print("card chosen");
        print(c);
        
        // c was chosen, send to server and disable all inputs
        print("cardchosen called local player");
        foreach (GameObject cm in hand)
        {
            cm.GetComponent<BoxCollider2D>().enabled = false;
        }
        GameObject c_gm = c.gameObject;
        hand.Remove(c_gm);
        c_gm.tag = MYTAG;
        c_gm.transform.position = new Vector3(-1, 3, 1);
        
        CmdCardChosen((int)c.card.robot, c.card.name, GetComponent<NetworkIdentity>().GetInstanceID(), GameObject.FindGameObjectsWithTag(OPPTAG).Length > 0);
    }

    [Command]
    private void CmdCardChosen(int robot, string name, int clientId, bool oppSentCard)
    {
        print("CmdCardChosen");
        RpcCardChosen(robot, name, clientId);

        // if both people chose their card, execute turn
        if(oppSentCard)
        {
            RpcExecuteTurn();
        }
    }

    [ClientRpc]
    void RpcCardChosen(int robot, string name, int clientId)
    {
        print("RpcCardChosen");
        int myAddr = GetComponent<NetworkIdentity>().GetInstanceID();
        if (myAddr != clientId)
        {
            print("not the originator");

            GameObject oppCard = Instantiate(cardPrefab, new Vector3(1, 3, 1), Quaternion.identity) as GameObject;
            oppCard.tag = OPPTAG;
            print(oppCard);
            CardModel cm = oppCard.GetComponent<CardModel>();
            cm.card = new Card((Card.Robot)robot, name);
            cm.ToggleFace(true);
            cm.setGameController(this);
        }
    }

    [ClientRpc]
    void RpcExecuteTurn()
    {
        print("RpcExecuteTurn()");
        // do game logic

        // remove used cards
        foreach(GameObject go in GameObject.FindGameObjectsWithTag(OPPTAG))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(MYTAG))
        {
            Destroy(go);
        }

        foreach (GameObject cm in hand)
        {
            cm.GetComponent<BoxCollider2D>().enabled = true;
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
