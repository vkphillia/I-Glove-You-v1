using UnityEngine;
using System.Collections;

public class NextPlayerBtn : MonoBehaviour
{
    public int id;
    public SwipePlayer swipePLayer;

    void OnMouseDown()
    {
        if (id == 1)
        {
            swipePLayer.MoveRight();
        }
        else if (id == 2)
        {
            swipePLayer.MoveLeft();
        }
    }
}
