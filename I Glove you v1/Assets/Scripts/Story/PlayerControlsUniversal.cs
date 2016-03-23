using UnityEngine;
using System.Collections;

public class PlayerControlsUniversal : MonoBehaviour
{
    public float mySpeed;

    [HideInInspector]
    public bool move;

    //this is temporary code, to be removed when new assets are added
    void Start()
    {
        StartCoroutine(FadePlayer());
    }

    IEnumerator FadePlayer()
    {
        Color tempColor = GetComponent<SpriteRenderer>().color;
        tempColor.a = 0;
        GetComponent<SpriteRenderer>().color = tempColor;
        yield return new WaitForSeconds(6f);
        while (tempColor.a<=1)
        {
            GetComponent<SpriteRenderer>().color = tempColor;
            tempColor.a += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


    }
    void Update()
    {
        KeyboardControls();
        MobileControls();

        if (move)
        {
            //why this? I just copied from ur code.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.75f, 2.75f), Mathf.Clamp(transform.position.y, -4.34f, 4.34f), 0);
            transform.position += transform.up * Time.deltaTime * mySpeed;
        }
    }


    void KeyboardControls()
    {
        
        if (Input.GetButton("movez"))
        {
            MoveClockWise();
        }

        else if (Input.GetButton("movex"))
        {
            MoveAntiClockWise();
        }
    }

    void MobileControls()
    {
        int count = Input.touchCount;
        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
            {
                MoveClockWise();
            }

            if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
            {
                MoveAntiClockWise();
            }
        }
    }


    void MoveClockWise()
    {
        transform.Rotate(0, 0, 5);
    }

    void MoveAntiClockWise()
    {
        transform.Rotate(0, 0, -5);
    }
}
