using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{
	void Update ()
	{
		
		transform.position += transform.up * Time.deltaTime * 10;
		
	}
}
