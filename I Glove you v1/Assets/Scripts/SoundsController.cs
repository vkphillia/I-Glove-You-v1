using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour
{
	#region Variable

	public static bool mute;
    
	public AudioSource[] bgSound;
	public AudioSource click;

	private Object[] sounds;
	private AudioSource[] audioSource = new AudioSource[5];

	#endregion

	#region Instance

	//Static Singleton Instance
	public static SoundsController _Instance = null;

	//property to get instance
	public static SoundsController Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{
				_Instance = (SoundsController)FindObjectOfType (typeof(SoundsController));
			}
			return _Instance;
		}
	}

	#endregion

	void Awake ()
	{
		if (_Instance != null && _Instance != this)
		{
			Destroy (this.gameObject);
		}
		else
		{
			_Instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	//initialization all the sound effects
	void Start ()
	{
		Invoke ("LoadAllSFX", 1f);
	}

	void LoadAllSFX ()
	{
		sounds = Resources.LoadAll ("Sounds", typeof(AudioClip));
		audioSource = GetComponents<AudioSource> ();
		Debug.Log (sounds.Length);
		//PlayBackgroundMusic (true, 0);//0 is for BG music
	}

	public void MuteSound ()
	{
		mute = !mute;
		if (mute)
		{
			if (OfflineManager.Instance != null)
			{
				if (OfflineManager.Instance.currentState == GameState.Paused)
				{
					PlayBackgroundMusic (false, 1);
					PlayBackgroundMusic (false, 0);

				}
			}
			else
			{
				PlayBackgroundMusic (false, 0);//0 is for BG music
			}
		}
		else
		{
			//if (OfflineManager.Instance.currentState == GameState.Playing)
			//{
			//	PlayBackgroundMusic (true, 0);//0 is for BG music
			//}
			if (OfflineManager.Instance != null)
			{
				if (OfflineManager.Instance.currentState == GameState.Paused)
				{
					PlayBackgroundMusic (true, 1);
					//PlayBackgroundMusic (true, 0);
				}
			}
			else
			{
				PlayBackgroundMusic (true, 0);//0 is for BG music
			}


		}
	}

	//for any button click sound
	public void PlayButtonClick ()
	{
		if (!mute)
		{
			click.Play ();
		}
	}

	//enable BG music and crowd sound, ID=0 is BG music, ID=1 is crowd sound
	public void PlayBackgroundMusic (bool start, int id)
	{
		if (!mute)
		{
			if (start)
			{
				bgSound [id].Play ();
			}
			else
			{
				bgSound [id].Stop ();
			}
		}
		else
		{
			bgSound [id].Stop ();
		}
	}

	//call this function with a parameter of the sound name as in resource folder
	public void PlaySoundFX (string sfxName, float vol)
	{
		if (!mute)
		{
			for (int i = 0; i < sounds.Length; i++)
			{
				if (sfxName == sounds [i].name)
				{
					if (!audioSource [0].isPlaying)
					{
						audioSource [0].clip = sounds [i] as AudioClip;
						audioSource [0].volume = vol;
						audioSource [0].Play ();
					}
					else if (!audioSource [1].isPlaying)
					{
						audioSource [1].clip = sounds [i] as AudioClip;
						audioSource [1].volume = vol;
						audioSource [1].Play ();
					}
					else if (!audioSource [2].isPlaying)
					{
						audioSource [2].clip = sounds [i] as AudioClip;
						audioSource [2].volume = vol;
						audioSource [2].Play ();
					}
					else if (!audioSource [3].isPlaying)
					{
						audioSource [3].clip = sounds [i] as AudioClip;
						audioSource [3].volume = vol;
						audioSource [3].Play ();
					}
					else
					{
						audioSource [4].clip = sounds [i] as AudioClip;
						audioSource [4].volume = vol;
						audioSource [4].Play ();
					}
					break;
				}

			}
		}
	}

	public void StopSoundFX (string sfxName)
	{
		if (!mute)
		{
			for (int i = 0; i < audioSource.Length; i++)
			{
				if (audioSource [i].isPlaying)
				{
					if (sfxName == audioSource [i].clip.name)
					{
					
						audioSource [i].Stop ();
						break;
					}
				}
			}
		}
	}
}
