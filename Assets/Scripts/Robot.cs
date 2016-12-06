using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

    private Card.Robot _robot;

    public Card.Robot robot
    {
        get { return _robot; }
        set { _robot = value; setSprite(); }
    }

    public Robot(Card.Robot r)
    {
        _robot = r;
        setSprite();
    }

    void setSprite()
    {
        switch (_robot)
        {
            case Card.Robot.Null:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Default - Character Card", typeof(Sprite)) as Sprite;
                break;
            case Card.Robot.DiscoFever:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Disco Fever - Character Card", typeof(Sprite)) as Sprite;
                break;
            case Card.Robot.TheOriginal:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("The Original - Character Card", typeof(Sprite)) as Sprite;
                break;
        }
    }

    public void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Default - Character Card", typeof(Sprite)) as Sprite;
    }
}
