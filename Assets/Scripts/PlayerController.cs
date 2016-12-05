using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, 0, z);
    }
}