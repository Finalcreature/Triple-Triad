using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] TextMesh[] allValues;
    [SerializeField] TextMesh[] rivalValues;
    [SerializeField] TextMesh[] ownValues;
    Card.Owner owner = Card.Owner.None;

    Card holdCard;
    
    int[] values;


    private void OnMouseDown()
    {
        
        if(!GameManager.selectedCard) { return; }
        
        holdCard = GameManager.selectedCard;
        owner = holdCard.owner;
        holdCard.transform.position = transform.position;
        values = holdCard.GetNums();

        //disable interactivity
        holdCard.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameManager.selectedCard = null;

        Calculation();

    }

    public void Calculation()
    {
        for (int i = 0; i < allValues.Length; i++)
        {
            allValues[i].text = values[i].ToString();
        }

        for (int i = 0; i < rivalValues.Length; i++)
        {
            if (rivalValues[i].text != "")
            {
                if (rivalValues[i].gameObject.GetComponentInParent<Detection>())
                {
                    Detection rivalCard = rivalValues[i].gameObject.GetComponentInParent<Detection>();
                    if (rivalCard.owner != owner)
                    {
                        if (int.Parse(ownValues[i].text) > int.Parse((rivalValues[i].text)))
                        {
                            holdCard.Owned(rivalCard.holdCard);
                            rivalCard.owner = rivalCard.holdCard.owner;
                            
                            //TODO if plus/same is active

                            //rivalCard.Calculation();
                        }

                    }

                }
            }
        }
    }
}
