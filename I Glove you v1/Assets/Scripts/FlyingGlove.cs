using UnityEngine;
using System.Collections;

public class FlyingGlove : MonoBehaviour
{

	private Vector3 takerPos;
	private bool fly;

	void Awake ()
	{
		GloveController.OnGlovePick += flyGlove;
	}

	void Update ()
	{
		if (fly)
		{
			transform.position = Vector3.MoveTowards (transform.position, takerPos, 5f);
		}
	}

	void flyGlove (PlayerHolderController giver, PlayerHolderController taker)
	{
		transform.position = giver.myPunchAnim.transform.position;
		takerPos = taker.transform.position;
		fly = true;
	}


	void OnDestroy ()
	{
		GloveController.OnGlovePick -= flyGlove;
	}


}
