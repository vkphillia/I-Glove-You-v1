using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashLogin : MonoBehaviour
{
    
	void Start ()
    {
        Invoke("LoadMenu", 3f);
	}
	
    void Update()
    {
        //just some loading effect //replace this before shipping
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time*5, 1f));
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("main menu");
    }
}
