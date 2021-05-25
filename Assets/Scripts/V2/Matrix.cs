using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    
    [SerializeField] GameObject slot;
    [SerializeField] NewCard card;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Instantiate(slot, new Vector2(i,j), transform.rotation);
            }
        }

      NewCard newCard = Instantiate(card, new Vector2(-1,-1), transform.rotation) as NewCard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    public enum GridCon
    { 
    Green,
    Red,
    None
    };

