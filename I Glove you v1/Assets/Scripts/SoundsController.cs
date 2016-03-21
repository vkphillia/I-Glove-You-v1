using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour
{
    //Static Singleton Instance
    public static SoundsController _Instance = null;

    //property to get instance
    public static SoundsController Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (SoundsController)FindObjectOfType(typeof(SoundsController));
            }
            return _Instance;
        }
    }

    private Object[] sounds;
    private AudioSource[] audioSource= new AudioSource[3];

    //initialization all the sound effects
    void Start ()
    {
        sounds = Resources.LoadAll("Sounds", typeof(AudioClip));
        audioSource = GetComponents<AudioSource>();

        //for debuggin only
        //for (int i=0;i<sounds.Length;i++)
        //{
            //Debug.Log(sounds[i].name+"\n");
        //}    
	}
	
    //call this function with a parameter of the sound name as in resource folder
	public void PlaySoundFX(string sfxName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sfxName==sounds[i].name)
            {
                if(!audioSource[0].isPlaying)
                {
                    audioSource[0].clip= sounds[i] as AudioClip;
                    audioSource[0].Play();
                }
                else if(!audioSource[1].isPlaying)
                {
                    audioSource[1].clip = sounds[i] as AudioClip;
                    audioSource[1].Play();
                }
                else
                {
                    audioSource[2].clip = sounds[i] as AudioClip;
                    audioSource[2].Play();
                }

                break;
            }
            
        }
    }
}
