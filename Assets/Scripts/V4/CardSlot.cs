using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Text[] allValues; //TODO check if I can change the variable type to int
    CardAttributes card;
    [SerializeField] Text[] rivalValues;
    [SerializeField] Text[] ownValues;
    int x, y;
    LevelManager levelManager;
    CardAttributes lCard, rCard, uCard, dCard;
    List<CardAttributes> sameValue = new List<CardAttributes>();
    List<CardAttributes> plusValues = new List<CardAttributes>();
    List<int> plusIndex = new List<int>();

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag && card == null)
        {
            card = eventData.pointerDrag.GetComponent<CardAttributes>();
            levelManager.GetSlot(this, out x, out y);



            //Method
            //print(levelManager.cardSlots[x + 1, y]);
            //print(levelManager.cardSlots[x - 1, y]);
            //print(levelManager.cardSlots[x , y + 1]);
            //print(levelManager.cardSlots[x , y - 1]);





            if (card.GetComponent<DragMechanic>().isDragable)
            {
                eventData.pointerDrag.GetComponent<DragMechanic>().isInplace = true;
                card.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                int[] values = card.GetNums();

                for (int i = 0; i < values.Length; i++)
                {
                    allValues[i].text = values[i].ToString();
                    allValues[i].CrossFadeAlpha(0, 0, false);
                }



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


                if (LevelManager.isSame && sameValue.Count > 1)
                {
                    for (int i = 0; i < sameValue.Count; i++)
                    {
                        card.Owned(sameValue[i], card);
                    }
                }


                if (LevelManager.isPlus)
                {
                    if(GetCardValue(2) + dCard.GetNum(0) == GetCardValue(3) + lCard.GetNum(1))
                    {
                        card.Owned(dCard, card);
                        card.Owned(lCard, card);
                    }
                    if(GetCardValue(2) + dCard.GetNum(0) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(dCard, card);
                        card.Owned(rCard, card);
                    }
                    if (GetCardValue(2) + dCard.GetNum(0) == GetCardValue(0) + uCard.GetNum(2))
                    {
                        card.Owned(dCard, card);
                        card.Owned(uCard, card);
                    }
                    if (GetCardValue(3) + lCard.GetNum(1) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(lCard, card);
                        card.Owned(rCard, card);
                    }
                    if (GetCardValue(3) + lCard.GetNum(1) == GetCardValue(0) + uCard.GetNum(2))
                    {
                        card.Owned(lCard, card);
                        card.Owned(uCard, card);
                    }
                    if (GetCardValue(0) + uCard.GetNum(2) == GetCardValue(1) + rCard.GetNum(3))
                    {
                        card.Owned(uCard, card);
                        card.Owned(rCard, card);
                    }



                    //    if (storeSet.Count == 4)
                    //    {
                    //        if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                    //        {
                    //            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //        }
                    //    }

                    //    else if (storeSet.Count == 6)
                    //    {
                    //        if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                    //        {
                    //            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //        }
                    //        else if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[4].text + storeSet[5].text))
                    //        {
                    //            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //        }
                    //        else if (int.Parse(storeSet[4].text + storeSet[5].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                    //        {
                    //            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //        }
                    //        else if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text)
                    //                 && int.Parse(storeSet[4].text + storeSet[5].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                    //        {
                    //            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                    //        }
                    //    }
                    //}

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
                else
                {
                    card = null;
                }
            }
        }
    }

    CardAttributes GetCard()
    {
        return card;
    }

    public int GetCardValue(int value)
    {
        return card.GetCard().GetNums(value);
    }

    //private void AddCardsToPool(List<Text> storeSet, int i)
    //{
    //    storeSet.Add(rivalValues[i]);
    //    storeSet.Add(ownValues[i]);
    //}


}
