using UnityEngine;
using System.Collections;

public class SwipePlayer : MonoBehaviour
{
    public Sprite[] players;
    public Sprite[] playersHighlighted;
    public GameObject[] windows;
    public GameObject[] nextButtons; 

    [HideInInspector]
    public int[] selectedID=new int[3];

    void Start()
    {
        windows[0].GetComponent<SpriteRenderer>().sprite = players[0];
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[1];
        //windows[1].GetComponent<SpriteRenderer>().sprite = players[1];
        windows[2].GetComponent<SpriteRenderer>().sprite = players[2];
        selectedID[0] = 0;
        selectedID[1] = 1;
        selectedID[2] = 2;
    }

    public void MoveLeft()
    {
        selectedID[0] = selectedID[1];
        selectedID[1] = selectedID[2];
        selectedID[2] += 1;

        windows[0].GetComponent<SpriteRenderer>().sprite = players[selectedID[0]];
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[selectedID[1]];

        if (selectedID[2] == players.Length)
        {
            windows[2].GetComponent<SpriteRenderer>().enabled = false;
            nextButtons[1].GetComponent<SpriteRenderer>().color = Color.grey;
            nextButtons[1].GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            windows[2].GetComponent<SpriteRenderer>().sprite = players[selectedID[2]];
        }

        
        //windows[1].GetComponent<SpriteRenderer>().sprite = players[selectedID[1]];
        //if ((selectedID[1]+1) == players.Length)
        //{
        //    Debug.Log("Left="+selectedID[2] + " " + players.Length);
        //    //nextButtons[1].SetActive(false);
            
        //}
        if(selectedID[0] == 0)
        {
            //nextButtons[0].SetActive(true);
            windows[0].GetComponent<SpriteRenderer>().enabled = true;
            nextButtons[0].GetComponent<SpriteRenderer>().color = Color.white;
            nextButtons[0].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void MoveRight()
    {
        selectedID[2] = selectedID[1];
        selectedID[1] = selectedID[0];
        selectedID[0] -= 1;
        
        windows[1].GetComponent<SpriteRenderer>().sprite = playersHighlighted[selectedID[1]];
        windows[2].GetComponent<SpriteRenderer>().sprite = players[selectedID[2]];

        if (selectedID[0] == -1)
        {
            windows[0].GetComponent<SpriteRenderer>().enabled = false;
            nextButtons[0].GetComponent<SpriteRenderer>().color = Color.grey;
            nextButtons[0].GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            
            windows[0].GetComponent<SpriteRenderer>().sprite = players[selectedID[0]];
        }

        //windows[1].GetComponent<SpriteRenderer>().sprite = players[selectedID[1]];
        

        //if (selectedID[0] == 0)
        //{
        //    Debug.Log("Right=" + selectedID[0] + " " + players.Length);
        //    //nextButtons[0].SetActive(false);
            
        //}
        if(selectedID[2]==(players.Length-1))
        {
            //nextButtons[1].SetActive(true);
            windows[2].GetComponent<SpriteRenderer>().enabled = true;
            nextButtons[1].GetComponent<SpriteRenderer>().color = Color.white;
            nextButtons[1].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
