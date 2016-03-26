using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public PlayerControlsUniversal player;
    public GameObject UI;

    public string[] dialouges;
    public Text dialougeText;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartTutorial());
	}
	
	IEnumerator StartTutorial()
    {
        for(int i=0;i<dialouges.Length;i++)
        {
            yield return new WaitForSeconds(3f);
            dialougeText.text = dialouges[i];
        }
        player.mySpeed = 0;
        player.move = true;
        yield return new WaitForSeconds(4f);
        dialougeText.text = "and now lets move";
        player.mySpeed = 4;
        yield return new WaitForSeconds(2f);
        dialougeText.text = "yes, we got it covered for you :)";
        yield return new WaitForSeconds(3f);
        dialougeText.text = "Congrats, you are all done with tutorial";
        player.move = false;
        UI.SetActive(true);

    }
}
