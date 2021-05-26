using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] CardAttributes[] cards = new CardAttributes[5];
    public static bool isSame, isPlus;
    [SerializeField] Button startButton;
    [SerializeField] Pack[] decks;
    [SerializeField] GameObject opponentsSelectScreen;
    bool isShowing;


    public CardSlot[,] cardSlots = new CardSlot[3, 3];


    public void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            CardCounter.rivaldeck = decks[0];
        }
       else
        {
            int index = 0;
            string num;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    num = index.ToString();
                    cardSlots[j, i] = GameObject.Find((index + 1).ToString()).GetComponent<CardSlot>();
                    index++; 
                }
            }
        }
    }

    public CardSlot GetSlot(CardSlot cardSlot)
    {
        foreach(CardSlot slot in cardSlots)
        {
            if(cardSlot == slot)
            {
                return slot;
            }
        }
        return null;
    }

    public void GetSlot(CardSlot cardSlot,out int num, out int num2)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (cardSlots[j, i] == cardSlot)
                {
                    num = j;
                    num2 = i;
                    return;
                }
               
            }
        }
        throw new System.Exception("Wrong para");
    }

    

    public void SetOpponent(int value)
    {
        CardCounter.SetRivalDeck(decks[value]);
    }

    public void ShowOpponentSelectPanel()
    {
        isShowing = !isShowing;
        opponentsSelectScreen.SetActive(isShowing);
    }


    public void SetSame(bool value)
    {
        isSame = value;
    }

    public void SetPlus(bool value)
    {
        isPlus = value;
    }

    public bool AddCard(CardAttributes myCard)
    {

        if (!isCardExist(myCard))
        {
            CardCounter.SetDeck(cards);
            CheckDeck();
            return true;

        }
        else
            return false;
    }

    public void CheckDeck()
    {
        foreach(CardAttributes card in cards)
        {
            if (card.GetCard() == null)
            {
                return;
            }
        }

        startButton.interactable = true;

    }


    public bool isCardExist(CardAttributes myCard)
    {
        bool isExist = false;
        foreach (CardAttributes card in cards)
        {
            if(card == myCard)
            {
                isExist = false;
            }
            else if(card.GetCard() == myCard.GetCard())
            {
                isExist = true;
                break;
            }
            else
            {
                isExist = false;
            }
        }

        return isExist;
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
        isPlus = false;
        isSame = false;
    }

    public string GetLevel()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
