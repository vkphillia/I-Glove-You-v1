using UnityEngine;
using System.Collections;

public class SwipePlayer : MonoBehaviour
{
    public Sprite[] players;
    public Sprite[] playersHighlighted;
    public GameObject[] windows;
    [HideInInspector]
    public int[] selectedID=new int[3];

    void Start()
    {
        windows[0].GetComponent<SpriteRenderer>().sprite = players[0];
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[1];
        windows[2].GetComponent<SpriteRenderer>().sprite = players[2];
        selectedID[0] = 0;
        selectedID[1] = 1;
        selectedID[2] = 2;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("Trigger");
    //}

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    Debug.Log("Collision");
    //}

    public void MoveLeft()
    {
        selectedID[0] = selectedID[1];
        selectedID[1] = selectedID[2];
        selectedID[2] += 1;

        if (selectedID[2]==players.Length)
        {
            selectedID[2] = 0;
        }

        windows[0].GetComponent<SpriteRenderer>().sprite = players[selectedID[0]];
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[selectedID[1]];
        windows[2].GetComponent<SpriteRenderer>().sprite = players[selectedID[2]];
    }

    public void MoveRight()
    {
        selectedID[2] = selectedID[1];
        selectedID[1] = selectedID[0];
        selectedID[0] -= 1;
        
        if (selectedID[0] == -1)
        {
            selectedID[0] = (players.Length-1);
        }

        windows[0].GetComponent<SpriteRenderer>().sprite = players[selectedID[0]];
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[selectedID[1]];
        windows[2].GetComponent<SpriteRenderer>().sprite = players[selectedID[2]];
    }
}
