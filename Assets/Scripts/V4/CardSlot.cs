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
    //static bool isSame, isPlus;

    /*Plus and same works even if one of the cards is owned by the attacker
      TODO change the code according to that*/

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag && card == null)
        {
            
            card = eventData.pointerDrag.GetComponent<CardAttributes>();

            if(card.GetComponent<DragMechanic>().isDragable)
            {
                eventData.pointerDrag.GetComponent<DragMechanic>().isInplace = true;
                card.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                int[] values = card.GetNums();

                for (int i = 0; i < values.Length; i++)
                {
                    allValues[i].text = values[i].ToString();
                    allValues[i].CrossFadeAlpha(0, 0, false);
                }

                List<Text> storeSet = new List<Text>();

                for (int i = 0; i < ownValues.Length; i++)
                {
                    
                        
                    if (rivalValues[i].text != "")
                    { 
                        CardAttributes rival = rivalValues[i].gameObject.GetComponentInParent<CardSlot>().card;

                        if(LevelManager.isSame || LevelManager.isPlus)
                        {
                            AddCardsToPool(storeSet, i);
                            if (int.Parse(ownValues[i].text) > int.Parse(rivalValues[i].text))
                            {
                                card.Owned(rival, card);
                            }
                        }

                        else if (rival.owner != card.owner)
                        {
                            AddCardsToPool(storeSet, i);
                            if (int.Parse(ownValues[i].text) > int.Parse(rivalValues[i].text))
                            {
                                card.Owned(rival);
                            }
                        }
                    }
                        
                        
                }
                

                if (LevelManager.isSame)
                {                   
                    if (storeSet.Count == 4)
                    {
                        if (storeSet[0].text == storeSet[1].text && storeSet[2].text == storeSet[3].text)
                        {
                            card.Owned(rivalValues[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(rivalValues[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                    }

                    else if (storeSet.Count == 6)
                    {
                        if (storeSet[0].text == storeSet[1].text)
                        {
                            if (storeSet[2].text == storeSet[3].text && storeSet[4].text == storeSet[5].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);

                            }
                            else if (storeSet[2].text == storeSet[3].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if (storeSet[4].text == storeSet[5].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                        }

                        else if (storeSet[2].text == storeSet[3].text && storeSet[4].text == storeSet[5].text)
                        {
                            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                    }

                    else if (storeSet.Count == 8)
                    {
                        if (storeSet[0].text == storeSet[1].text)
                        {
                            if (storeSet[2].text == storeSet[3].text && storeSet[4].text == storeSet[5].text
                                && storeSet[6].text == storeSet[7].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[6].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if (storeSet[2].text == storeSet[3].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if (storeSet[4].text == storeSet[5].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if (storeSet[6].text == storeSet[7].text)
                            {
                                card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[6].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                        }
                        
                        else if (storeSet[2].text == storeSet[3].text)
                        {
                            if(storeSet[4].text == storeSet[5].text && storeSet[6].text == storeSet[7].text)
                            {
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[6].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if(storeSet[4].text == storeSet[5].text)
                            {
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                            else if(storeSet[6].text == storeSet[7].text)
                            {
                                card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                                card.Owned(storeSet[6].gameObject.GetComponentInParent<CardSlot>().card, card);
                            }
                        }
                        
                        else if(storeSet[4].text == storeSet[5].text && storeSet[6].text == storeSet[7].text)
                        {
                            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[6].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                    }
                }

                if(LevelManager.isPlus)
                {
                    
                    if (storeSet.Count == 4)
                    {
                        if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                        {
                            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                    }

                    else if (storeSet.Count == 6)
                    {
                        if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                        {
                            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                        else if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[4].text + storeSet[5].text))
                        {
                            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                        else if (int.Parse(storeSet[4].text + storeSet[5].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                        {
                            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                        else if (int.Parse(storeSet[0].text + storeSet[1].text) == int.Parse(storeSet[2].text + storeSet[3].text)
                                 && int.Parse(storeSet[4].text + storeSet[5].text) == int.Parse(storeSet[2].text + storeSet[3].text))
                        {
                            card.Owned(storeSet[0].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[2].gameObject.GetComponentInParent<CardSlot>().card, card);
                            card.Owned(storeSet[4].gameObject.GetComponentInParent<CardSlot>().card, card);
                        }
                    }
                }

                CardCounter.turns++;
                if(CardCounter.turns > 9)
                {
                    if(CardCounter.playerCards > CardCounter.rivalCards)
                    {
                        print("Player win");
                    }
                    else if(CardCounter.playerCards < CardCounter.rivalCards)
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

    private void AddCardsToPool(List<Text> storeSet, int i)
    {
        storeSet.Add(rivalValues[i]);
        storeSet.Add(ownValues[i]);
    }


}
