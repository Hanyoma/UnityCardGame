using UnityEngine;
using UnityEngine.Networking;

public class CardModel : MonoBehaviour
{    
    private Card _card = null;
    private GameController _gc;

    public Card card
    {
        get { return _card; }
        set
        {
            _card = (Card) value;
            ToggleFace(false);
        }
    }
    
    public void setOpponent()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("back_blue", typeof(Sprite)) as Sprite;
    }

    public CardModel(Card c, GameController gc)
    {
        _card = c;
        _gc = gc;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("back_red", typeof(Sprite)) as Sprite;
    }
    
    public void setGameController(GameController gc)
    {
        _gc = gc;
    }
    
    public void ToggleFace(bool showFace)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = (showFace) ? Resources.Load(_card.name, typeof(Sprite)) as Sprite : Resources.Load("back_red", typeof(Sprite)) as Sprite;
    }
    
    public override string ToString()
    {
        return "CardModel: " + _card.ToString();
    }

    void OnMouseDown()
    {
        _gc.CardChosen(this);
    }
}
