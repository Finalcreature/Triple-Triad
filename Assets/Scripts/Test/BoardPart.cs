using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardPart : MonoBehaviour
{
    bool hasCard;

    private void OnMouseDown()
    {
        hasCard = true;
    }

    public bool GetCard()
    {
        return hasCard;
    }
}
