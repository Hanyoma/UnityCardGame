using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

    // The index of the current card in the faces array
    public int cardIndex;

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
