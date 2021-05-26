using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardAttributes : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public enum Owner { Player1, Player2, None };
    public Owner owner;
    [SerializeField] Card.ActualCard card; 
    [SerializeField] Text[] numText = new Text[4];
    [SerializeField] Image frame;
    [SerializeField] Sprite back;
    [SerializeField] int cardNum;
    [SerializeField] Button startButton;
    GameObject cardCover;

    public Card.ActualCard GetCard()
    {
        return card;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (FindObjectOfType<LevelManager>().GetLevel() == "Select Screen")
        {
            RemoveCard();
            startButton.interactable = false;
        }
    }

   void Start()
    {
        
        if(FindObjectOfType<LevelManager>().GetLevel() != "Select Screen")
        {
            if(owner == Owner.Player1)
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

    void SetCard()
    {      
        GetComponent<Image>().sprite = card.GetPic();

        cardCover = transform.GetChild(1).gameObject;
        cardCover.SetActive(false);



        for (int i = 0; i < 4; i++)
        {
            numText[i].text = card.GetNums(i).ToString();
        }      

    }

    void RemoveCard()
    {
        cardCover = transform.GetChild(1).gameObject;
        cardCover.SetActive(true);



        for (int i = 0; i < 4; i++)
        {
            numText[i].text = "";
        }
    }

    public void Owned(CardAttributes card)
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

    public void ShowCover()
    {
        bool activate;
        activate =  (cardCover.activeInHierarchy)? false : true;
        cardCover.SetActive(activate);
        for (int i = 2; i < 6; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!activate);
        }
    }

   
}
