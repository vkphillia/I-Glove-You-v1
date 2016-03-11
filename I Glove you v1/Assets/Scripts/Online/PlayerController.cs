using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
	

	public Sprite[] playerSprites;

    private float mySpeed=5f;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    public void SetPlayerNumber(int playerNum)
    {
        GetComponent<SpriteRenderer>().sprite = playerSprites[playerNum - 1];
    }

    
}