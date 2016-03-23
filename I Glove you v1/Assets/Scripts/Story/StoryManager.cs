using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    //scripts
    public PlayerControlsUniversal playerControl;

    public Text story;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(GameIntro());
	}
	
    IEnumerator GameIntro()
    {
        story.text = "";
        yield return new WaitForSeconds(2f);
        story.text = "Hi";
        yield return new WaitForSeconds(2f);
        story.text = "How are you?";
        yield return new WaitForSeconds(2f);
        story.text = "This is your player";
        yield return new WaitForSeconds(3f);
        story.text = "Enough watching\n Let's learn player movements";
        yield return new WaitForSeconds(4f);
        story.text = "Press key Z or tap left to rotate player in anticlockwise direction";
        yield return new WaitForSeconds(5.5f);
        story.text = "Great";
        yield return new WaitForSeconds(1f);
        story.text = "Press key X or tap right to rotate player in clockwise direction";
        yield return new WaitForSeconds(5.5f);
        story.text = "Great";
        yield return new WaitForSeconds(1.5f);
        story.text = "Now dont worry about the movements, we got it covered for you";
        yield return new WaitForSeconds(4.5f);
        story.text = "See";
        playerControl.move = true;
        yield return new WaitForSeconds(1f);
        story.text = "";
        yield return new WaitForSeconds(7f);
        story.text = "Congats, you learned about your player movements";
        playerControl.move = false;
        yield return new WaitForSeconds(4f);
        story.text = "Come back later for more :)";
    }
}
