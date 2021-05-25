using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public enum Owner { Player1, Player2, None };
    public Owner owner;
    [SerializeField] ActualCard card;

    public bool isMoveable;

    GameObject rivalCard;
    
    [SerializeField] TextMesh[] numText = new TextMesh[4];

    List<Card> rivalCards;

    Card[] AllCards;


    private void Start()
    {


        for (int i = 0; i < 4; i++)
        {
            numText[i].text = card.GetNums(i).ToString();
            // nums[i] = int.Parse(numText[i].text);
        }

        if (GetComponent<SpriteRenderer>().color == Color.green)
        {
            owner = Owner.Player1;
        }
        else
        {
            owner = Owner.Player2;
        }

        isMoveable = true;

    }

    public int GetNum(string side)
    {
        switch (side)
        {
            case "Up": return card.GetNums(0);
            case "Right": return card.GetNums(1);
            case "Down": return card.GetNums(2);
            default: return card.GetNums(3);
        }
    }

    public int[] GetNums()
    {
        return card.GetNums();
    }

    private void OnMouseDown()
    {

        if (isMoveable)
        {
            GameManager.selectedCard = this;
            isMoveable = false;
        }

    }

    //public void Check()
    //{
    //    // AllCards = FindObjectsOfType<Card>();
        
    //    rivalCards = new List<Card>();
        
    //    foreach (Card cardOnGrid in GameManager.allCards)
    //    {
    //        if (cardOnGrid.owner != owner)
    //        {
    //            rivalCards.Add(cardOnGrid);
    //        }
    //    }
        
    //    foreach (Card rivalCard in rivalCards)
    //    {
            

    //        if (Mathf.Abs(rivalCard.transform.position.x - transform.position.x) == 1 && rivalCard.transform.position.y == transform.position.y||
    //            Mathf.Abs(rivalCard.transform.position.y - transform.position.y) == 1.25f && rivalCard.transform.position.x == transform.position.x)
    //        {

    //           // print(rivalCard.name);
    //            if(rivalCard.transform.position.x > transform.position.x)
    //            {
    //                if (rivalCard.GetNum("Left") < GetNum("Right"))
    //                {
    //                    Owned(rivalCard);
    //                }

    //            }
            
    //            else if(rivalCard.transform.position.x < transform.position.x)
    //            {
    //                if (rivalCard.GetNum("Right") < GetNum("Left"))
    //                {
    //                    Owned(rivalCard);
    //                }
    //            }

    //            else if (rivalCard.transform.position.y > transform.position.y)
    //            {
    //                if (rivalCard.GetNum("Down") < GetNum("Up"))
    //                {
    //                    Owned(rivalCard);
    //                }
    //            }

    //            else
    //            {
    //                if (rivalCard.GetNum("Up") < GetNum("Down"))
    //                {
    //                    Owned(rivalCard);
    //                }
    //            }
    //        }       
    //    }
    //}


    public void Owned(Card card)
    {

        if(card.owner == Owner.Player1)
        {
            card.owner = Owner.Player2;
            card.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            card.owner = Owner.Player1;
            card.GetComponent<SpriteRenderer>().color = Color.green;
           
        }
    }

    [CreateAssetMenu(menuName = "Card")]
    public class ActualCard : ScriptableObject
    {

        [SerializeField] int[] nums = new int[4];
        [SerializeField] Sprite pic;

        public int GetNums(int index)
        {
            return nums[index];
        }

        public int[] GetNums()
        {
            return nums;
        }

        public Sprite GetPic()
        {
            return pic;
        }
 
    }
}

[CreateAssetMenu(menuName = "Deck")]
public class Pack : ScriptableObject
{
    [SerializeField] Card.ActualCard[] cards;

    public Card.ActualCard GetCard(int index)
    {
        return cards[index];
    }
}
