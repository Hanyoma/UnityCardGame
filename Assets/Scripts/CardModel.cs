using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CardModel : NetworkBehaviour {
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

    // The index of the current card in the faces array
    [SyncVar(hook = "IndexChanged")]
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

    void OnGUI()
    {

        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            print("PRESSED A BUTTON");

            if (!isLocalPlayer)
            {
                return;
            }
            cardIndex = Random.Range(0, 52);

        }
    }

    void IndexChanged(int index)
    {
        print("Updating image");
        ToggleFace(true);
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
