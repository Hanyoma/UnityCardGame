using UnityEngine;
using UnityEngine.Networking;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    private Card _card = null;
    private GameController MyGameController;
    
    private int cardIndex = 0;

    public Card card
    {
        get { return _card; }
        set { _card = (Card) value;
        }
    }

    public CardModel(Card c, GameController gc)
    {
        _card = c;
        MyGameController = gc;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("back_red", typeof(Sprite)) as Sprite;
    }
    
    void Awake()
    {
        MyGameController = GetComponent<GameController>() as GameController;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleFace(bool showFace)
    {
        print("toggled");
        gameObject.GetComponent<SpriteRenderer>().sprite = (showFace) ? Resources.Load(_card.name, typeof(Sprite)) as Sprite : Resources.Load("back_red", typeof(Sprite)) as Sprite;
    }
    
    public override string ToString()
    {
        return "CardModel: " + _card.ToString();
    }

    public void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            print(this);
            // card was clicked, notify GameController
            GetComponent<GameController>().CardChosen(this);
        }
        */
    }
}
