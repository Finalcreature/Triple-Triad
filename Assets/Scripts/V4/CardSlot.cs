using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IDropHandler
{
    CardAttributes card;
    int x, y;
    LevelManager levelManager;
    CardAttributes lCard, rCard, uCard, dCard; //Card neighbors
    List<CardAttributes> sameValue = new List<CardAttributes>();

    //TODO check if relevant
    List<CardAttributes> plusValues = new List<CardAttributes>();
    List<int> plusIndex = new List<int>();

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Check if something is being drag and it contains a card
        if (eventData.pointerDrag && card == null)
        {
            card = eventData.pointerDrag.GetComponent<CardAttributes>();
            levelManager.GetSlot(this, out x, out y); //Set card slot
            
            //The card will be dragable only if it's the turn of the attacker
            if (card.GetComponent<DragMechanic>().isDragable)
            {
                eventData.pointerDrag.GetComponent<DragMechanic>().isInplace = true;
                card.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; //Place the card in the in the slot
                int[] values = card.GetNums();
      
                Comparison();
                SameRule();
                PlusRule();
                CheckResult();
            }
            else //Remove card to prevent overlap
            {
                card = null; 
            }
        }
    }

    //TODO find a way to improve this
    //Compare card to its surroundings
    private void Comparison()
    {
        if (y != 0 && levelManager.cardSlots[x, y - 1].card)
        {
            dCard = levelManager.cardSlots[x, y - 1].card;
            if (GetCardValue(2) > dCard.GetNum(0))
            {
                card.Owned(dCard, card);
            }
            else if (GetCardValue(2) == dCard.GetNum(0))
            {
                sameValue.Add(dCard);

            }
        }
        if (x != 0 && levelManager.cardSlots[x - 1, y].card)
        {
            lCard = levelManager.cardSlots[x - 1, y].card;
            if (GetCardValue(3) > lCard.GetNum(1))
            {
                card.Owned(lCard, card);
            }
            else if (GetCardValue(3) == lCard.GetNum(1))
            {
                sameValue.Add(lCard);

            }
        }

        if (x != 2 && levelManager.cardSlots[x + 1, y].card)
        {
            rCard = levelManager.cardSlots[x + 1, y].card;
            if (GetCardValue(1) > rCard.GetNum(3))
            {
                card.Owned(levelManager.cardSlots[x + 1, y].card, card);
            }
            else if (GetCardValue(1) == rCard.GetNum(3))
            {
                sameValue.Add(rCard);

            }
        }

        if (y != 2 && levelManager.cardSlots[x, y + 1].card)
        {
            uCard = levelManager.cardSlots[x, y + 1].card;
            if (GetCardValue(0) > uCard.GetNum(2))
            {
                card.Owned(uCard, card);
            }
            else if (GetCardValue(0) == uCard.GetNum(2))
            {
                sameValue.Add(uCard);

            }
        }
    }
    private void PlusRule()
    {
        if (LevelManager.isPlus)
        {
            if(dCard)
            {
                if(lCard)
                { 
                    if (GetCardValue(2) + dCard.GetNum(0) == GetCardValue(3) + lCard.GetNum(1))
                    {
                        card.Owned(dCard, card);
                        card.Owned(lCard, card);
                    }
                }
                if(rCard)
                {
                    if (GetCardValue(2) + dCard.GetNum(0) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(dCard, card);
                        card.Owned(rCard, card);
                    }
                }
                if(uCard)
                {
                    if (GetCardValue(2) + dCard.GetNum(0) == GetCardValue(0) + uCard.GetNum(2))
                    {
                        card.Owned(dCard, card);
                        card.Owned(uCard, card);
                    }
                }
            }
            if(lCard)
            {
                if(rCard)
                {
                    if (GetCardValue(3) + lCard.GetNum(1) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(lCard, card);
                        card.Owned(rCard, card);
                    }
                }
                if(uCard)
                {
                    if (GetCardValue(3) + lCard.GetNum(1) == GetCardValue(0) + uCard.GetNum(2))
                    {
                        card.Owned(lCard, card);
                        card.Owned(uCard, card);
                    }
                }
            }
            if(uCard)
            {
                if(rCard)
                {
                    if (GetCardValue(0) + uCard.GetNum(2) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(uCard, card);
                        card.Owned(rCard, card);
                    }
                }
            }
            
            
            

        }
    }
    private void SameRule()
    {
        if (LevelManager.isSame && sameValue.Count > 1)
        {
            for (int i = 0; i < sameValue.Count; i++)
            {
                card.Owned(sameValue[i], card);
            }
        }
    }
    private static void CheckResult()
    {
        CardCounter.turns++;
        if (CardCounter.turns > 9)
        {
            if (CardCounter.playerCards > CardCounter.rivalCards)
            {
                print("Player win");
            }
            else if (CardCounter.playerCards < CardCounter.rivalCards)
            {
                print("Player lose");
            }
            else
            {
                print("Draw");
            }
        }
    }
    public int GetCardValue(int value)
    {
        return card.GetCard().GetNums(value);
    }
    CardAttributes GetCard()
    {
        return card;
    }

}
