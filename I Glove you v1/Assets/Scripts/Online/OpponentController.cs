using UnityEngine;
using System.Collections;

public class OpponentController : MonoBehaviour
{

    public Sprite[] playerSprites;
    

    public void SetPlayerNumber(int playerNum)
    {
        GetComponent<SpriteRenderer>().sprite = playerSprites[playerNum - 1];
    }

    public void SetCarInformation(float posX, float posY, float rotZ)
    {
        OnlineManager.Instance.debugMessagesLobbby.text="setting opponent car: "+posX+" "+posY+" "+rotZ;

        transform.position = new Vector3(posX, posY, 0);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // We're going to do nothing with velocity.... for now
    }
}
