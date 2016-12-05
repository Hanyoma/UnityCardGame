using UnityEngine;
using UnityEngine.Networking;

public class CardModel : NetworkBehaviour
{
    SpriteRenderer spriteRenderer;
    
    private Card _card = null;
    public GameController MyGameController;
    
    [SyncVar(hook="IndexChanged")]
    private int cardIndex = 0;

    public Card card
    {
        get { return _card; }
        set { _card = (Card) value; }
    }

    public CardModel(Card c, GameController gc)
    {
        _card = c;
        MyGameController = gc;
        gameObject.GetComponent<SpriteRenderer>().sprite = 
            (localPlayerAuthority) ? Resources.Load("back_red",  typeof(Sprite)) as Sprite :
                                     Resources.Load("back_blue", typeof(Sprite)) as Sprite;
    }
    
    void Awake()
    {
        MyGameController = GetComponent<GameController>() as GameController;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleFace(bool showFace)
    {
        print("toggled");
        if (!showFace)
            spriteRenderer.sprite =
            (localPlayerAuthority) ? Resources.Load("back_red", typeof(Sprite)) as Sprite :
                                     Resources.Load("back_blue", typeof(Sprite)) as Sprite;
        else
        {
            //cardIndex = ((int)_card.suit) * 13 + _card.value - 1;
        }
    }

    void IndexChanged(int index)
    {
        //spriteRenderer.sprite = faces[index];
    }

    public override string ToString()
    {
        return "CardModel: " + _card.ToString();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // card was clicked, notify GameController
            MyGameController.CardChosen(this);
        }
    }
}
