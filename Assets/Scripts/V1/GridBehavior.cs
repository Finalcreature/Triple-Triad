using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    int xLimit = 1;
    float yLimit = 1.25f;
    public enum Place { bL,bM,bR,mL,mM,mR,tL,tM,tR};
    [SerializeField] Place place;
    [SerializeField] GridBehavior[] neighbors;

    Vector3 location;
    Card holdCard;
    
    void Start()
    {
        location = transform.position;
        neighbors = new GridBehavior[4];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(GameManager.selectedCard)
        {
            holdCard = GameManager.selectedCard;
            holdCard.transform.position = location;
            holdCard.transform.Translate(Vector3.back);
            GameManager.allCards.Add(GameManager.selectedCard);
            //holdCard.Check();
            GameManager.selectedCard = null;
            print(holdCard.name);
            GetNeighbors();

            
        }
    }

    public void GetNeighbors()
    {
        int slotIndex = 0;
        switch (place)
        {
            case Place.bL:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if(GameManager.slots[i].place == Place.bM || GameManager.slots[i].place == Place.mL)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }

                    foreach (GridBehavior neighbor in neighbors)
                    {
                        if(neighbor)
                        {
                            if (neighbor.transform.position.x > transform.position.x)
                            {
                                print(neighbor.GetCard().GetNum("Left"));
                            }
                            else
                            {
                                print(neighbor.GetCard().GetNum("Down"));
                            }
                        }
                    }
                }
                break;

            case Place.bM:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.bL || GameManager.slots[i].place == Place.bR ||
                            GameManager.slots[i].place == Place.mM)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.bR:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.bM || GameManager.slots[i].place == Place.mR)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.mL:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.bL || GameManager.slots[i].place == Place.mM ||
                            GameManager.slots[i].place == Place.tL)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.mM:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.bM || GameManager.slots[i].place == Place.mR ||
                            GameManager.slots[i].place == Place.mL || GameManager.slots[i].place == Place.tM)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.mR:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.bR || GameManager.slots[i].place == Place.mM ||
                            GameManager.slots[i].place == Place.tR)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.tL:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.mR || GameManager.slots[i].place == Place.tM)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.tM:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.tL|| GameManager.slots[i].place == Place.mM ||
                            GameManager.slots[i].place == Place.tR)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
            case Place.tR:
                {
                    for (int i = 0; i < GameManager.slots.Length; i++)
                    {
                        if (GameManager.slots[i].place == Place.mR || GameManager.slots[i].place == Place.tM)
                        {
                            neighbors[slotIndex] = GameManager.slots[i];
                            slotIndex++;
                        }
                    }
                }
                break;
        }
    }

    public Card GetCard()
    {
        if(holdCard)
        {
         return holdCard;

        }
        else
        {
            return null;
        }
    }


}
