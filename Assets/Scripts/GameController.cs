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
    
    public bool disableCards;

    void Awake()
    {
    }

    public override void OnStartLocalPlayer()
    {
        DontDestroyOnLoad(this);
    }

    public void makeDeck()
    {
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
                break;

        }
        for (int i = 0; i < 10; ++i)
        {
            Instantiate(cardPrefab, new Vector3(-7 + i * 2, 0, 0), Quaternion.identity);
        }
    }

    public void onRobot_SelectClick()
    {
        print("onRobot_SelectClick()");
        if (!isLocalPlayer) return;
        print("is local player");

        switch(GameObject.Find("Robot_Dropdown").GetComponent<Dropdown>().value)
        {
            case 0:
                print("0");
                // The Original
                robot = Card.Robot.TheOriginal;
                break;
            case 1:
                print("1");
                // Disco Fever
                robot = Card.Robot.DiscoFever;
                break;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        makeDeck();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
    }

    public void CardChosen(CardModel c)
    {
        // c was chosen, send to server and disable all inputs

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
