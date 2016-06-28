using UnityEngine;
using System.Collections;

public class parallaxGloves : MonoBehaviour
{

	void Update ()
	{
		transform.position += transform.up * Time.deltaTime * 2f;
		if (transform.position.y >= 10f)
		{
			transform.position = new Vector3 (0, -11.36f, 0);
		}
	}
}
