﻿using UnityEngine;
using System.Collections;

public delegate void PUPickedEvent ();
public class PowerUp : MonoBehaviour
{
	public static event PUPickedEvent OnPUPick;

	//This is a base call and all Power ups need to be derived from this class

	public int weight;
	public ParticleSystem myPS;
	public bool active;

	public GameObject myMarker;
	public Color myMarkerColor;

	public Animator myAnim;


	public virtual void OnEnable ()
	{
		myPS.gameObject.SetActive (true);
		StartCoroutine (AppearPU ());
	}

	public virtual void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing && !active)
		{
			transform.Rotate (0, 0, 2);
		}

	}

	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
		
			Player1Picked ();
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			
			Player2Picked ();
		}
		else if (other.gameObject.layer == 9 && this.gameObject.layer == 14)
		{
			if (!active)
			{
				Player1WithGlovePicked ();
				//Debug.Log ("boxerHitPU");
			}
		}
		else if (other.gameObject.layer == 11 && this.gameObject.layer == 14)
		{
			if (!active)
			{
				Player2WithGlovePicked ();
				//Debug.Log ("boxerHitPU");

			}
		}
	}



	public virtual void Player1Picked ()
	{
	
		Debug.Log ("Player1Health++");
	}

	public virtual void Player2Picked ()
	{
	
		Debug.Log ("Player2Health++");
	}

	public virtual void Player1WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void Player2WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
		DeactivatePU ();
	}

	public virtual void DeactivatePU ()
	{
		if (OnPUPick != null)
		{
			OnPUPick ();
		}
		transform.position = new Vector3 (-5, -3, 0);
		active = false;
		gameObject.SetActive (false);
	}

	public virtual void OnDisable ()
	{
		active = false;
		transform.position = new Vector3 (-5, -3, 0);
	}


	public virtual IEnumerator AppearPU ()
	{
		yield return new WaitForSeconds (.6f);
		myAnim.Play ("PU_Appear");
	}


}
