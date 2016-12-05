using UnityEngine;
using UnityEngine.Networking;

public class CardModel : NetworkBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;
    private Card _card;
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
    }

    public CardModel()
    {
        _card = null;
    }

    public void ToggleFace(bool showFace)
    {
        print("toggled");
        if (!showFace)
            spriteRenderer.sprite = cardBack;
        else
        {
            cardIndex = ((int)_card.suit) * 13 + _card.value - 1;
        }
    }

    void IndexChanged(int index)
    {
        spriteRenderer.sprite = faces[index];
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override string ToString()
    {
        return "CardModel: " + _card.ToString();
    }
}
