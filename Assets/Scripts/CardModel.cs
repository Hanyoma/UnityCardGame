using UnityEngine;
using UnityEngine.Networking;

public class CardModel : NetworkBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;
    private Card _card;
    public GameController MyGameController;

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
        spriteRenderer.sprite = (showFace) ? faces[((int)_card.suit) * 13 + _card.value - 1] : cardBack;
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override string ToString()
    {
        return "CardModel: " + _card.ToString();
    }
    
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, 0, z);
    }
}
