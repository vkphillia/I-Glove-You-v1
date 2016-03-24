using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge4 : MonoBehaviour
{
    public PlayerControlsUniversal player;
    public Text filler;

    void Start()
    {
        //all these things are temporary
        
        StartCoroutine(stopRound());
    }

    //temporay codes just to give an idea
    IEnumerator stopRound()
    {
        filler.text = "Challenge 4";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "Show him what you are";
        player.move = true;
        yield return new WaitForSeconds(7f);
        player.move = false;
        filler.text = "stop\n Taking you to the another challenge";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("5th challenge");
    }

}
