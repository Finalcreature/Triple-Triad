using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardAttributes : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public enum Owner { Player1, Player2, None }; //None for the empty slots
    public Owner owner;
    [SerializeField] Card.ActualCard card; //Card properties stored in Card script
    [SerializeField] Text[] numText = new Text[4]; //Represent all 4 number from the top clockwise
    [SerializeField] Image frame;
    [SerializeField] Sprite back; //Card's cover image
    [SerializeField] int cardNum;
    [SerializeField] Button startButton;
    GameObject cardCover;

    public Card.ActualCard GetCard()
    {
        return card;
    }

    public void SetCard(Card.ActualCard value)
    {
        card = value;
        SetCard();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag)
        {
            card = eventData.pointerDrag.GetComponent<Drag>().card;
            if (FindObjectOfType<LevelManager>().AddCard(this))
            {
                SetCard();
            }
        }
    }

    //During the setup phase
    public void OnPointerDown(PointerEventData eventData)
    {
        if (FindObjectOfType<LevelManager>().GetLevel() == "Select Screen")
        {
            RemoveCard();
            startButton.interactable = false;
        }
    }

    //Get the card from the deck
    //Change the frame of the card according to owner
   void Start()
    {
        
        if(FindObjectOfType<LevelManager>().GetLevel() != "Select Screen")
        {
            if (owner == Owner.Player1)
            {
                card = CardCounter.cards[cardNum].GetCard();
            }
            else
            {
                card = CardCounter.rivaldeck.GetCard(cardNum);
            }
                SetCard();

                if (owner == Owner.Player1)
                {
                    frame.color = Color.green;
                }
                else
                {
                    frame.color = Color.red;
                }
            
        }
    }

    //Set the image of the designated card 
    //Set the card cover while turning it off
    //Set the values of the card on text
    public void SetCard()
    {      
        GetComponent<Image>().sprite = card.GetPic();

        cardCover = transform.GetChild(1).gameObject;
        cardCover.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            numText[i].text = card.GetNums(i).ToString();
        }      

    }

    //Removing card from deck
    //Turning on the cover
    //Reset text on card's deck
    void RemoveCard()
    {
        cardCover = transform.GetChild(1).gameObject;
        cardCover.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            numText[i].text = "";
        }
    }

    //Change card ownership if card is not equal to the attacking card
    public void Owned(CardAttributes card, CardAttributes ownCard)
    {
        if(card)
        {
            if(card.owner != ownCard.owner)
            {
                card.gameObject.GetComponent<Animation>().Play();

                if (card.owner == Owner.Player1)
                {
                    card.owner = Owner.Player2;
                    StartCoroutine(ChangeColour(card, false));
                    CardCounter.playerCards--;
                    CardCounter.rivalCards++;
                }
                else
                {
                    card.owner = Owner.Player1;
                    StartCoroutine(ChangeColour(card, true));
                    CardCounter.playerCards++;
                    CardCounter.rivalCards--;
                }
            }
        }
    }

    //Delay the change of the colour
    IEnumerator ChangeColour(CardAttributes card, bool isPlayer1)
    {
        yield return new WaitForSeconds(0.3f);
        card.frame.color = (isPlayer1) ? Color.green : Color.red;
    }

    public int[] GetNums()
    {
        return card.GetNums();
    }

    public int GetNum(int value)
    {
        return card.GetNums(value);
    }

    //Called from animation 
    public void ShowCover()
    {
        bool activate;
        activate = (cardCover.activeInHierarchy) ? false : true;
        cardCover.SetActive(activate);
        for (int i = 2; i < 6; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!activate);
        }
    }


}
