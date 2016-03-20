using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour
{
    Object[] sounds;

    // Use this for initialization
    void Start ()
    {
        sounds = Resources.LoadAll("Sounds", typeof(AudioClip));
        
        //for debuggin only
        for (int i=0;i<sounds.Length;i++)
        {
            //Debug.Log(sounds[i].name+"\n");
        }    
	}
	
	public void PlaySoundFX(string sfxName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sfxName==sounds[i].name)
            {
                GetComponent<AudioSource>().clip = sounds[i] as AudioClip;
                GetComponent<AudioSource>().Play();
                break;
            }
            
        }
    }
}
