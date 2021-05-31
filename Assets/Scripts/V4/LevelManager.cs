using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] CardAttributes[] cards = new CardAttributes[5]; //Set the deck cards manually
    public static bool isSame, isPlus;
    [SerializeField] Button startButton;
    [SerializeField] Pack[] decks; //Stored decks (scriptable objects)
    [SerializeField] GameObject opponentsSelectScreen; //Panel
    bool isShowing; //Panel is showing
    public Sprite[] flowText;

    [SerializeField] GameObject[] defaultCards;


    public CardSlot[,] cardSlots = new CardSlot[3, 3]; //Matrix of card slots


    public void Start()
    {
        //If the player doesn't set the opponent it will be the first deck by default
        //TODO change so the deck will be chosed randomly (Random.Range(0,decks.length+1)
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            CardCounter.rivaldeck = decks[0];
            if(CardCounter.cards != null)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].SetCard(CardCounter.cards[i].GetCard());                  
                }
                GameObject.Find("Start Button").GetComponent<Button>().interactable = true;
            }
        }
       else
        {
            //Set card slots according to hierarchy
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cardSlots[j, i] = GameObject.Find((index + 1).ToString()).GetComponent<CardSlot>();
                    index++; 
                }
            }

            GameObject.Find("Flow Text").GetComponent<Animator>().SetTrigger("GameStart");
            GameObject.Find("Flow Text").GetComponent<Image>().sprite = flowText[2];

            StartCoroutine("DrawCards");
        }
    }

    IEnumerator DrawCards()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked;
        int index;
        bool active;
        int turn = 0;
        yield return new WaitForSeconds(3f);
        foreach (GameObject deafultCard in defaultCards)
        {
            index = Random.Range(0, 2);
            deafultCard.SetActive(true);
            active = (index == 1) ? true : false;
            deafultCard.transform.GetChild(0).gameObject.SetActive(active);
            yield return new WaitForSeconds(1.5f);
            turn += index;
        }

        CardCounter.isPlayerTurn = (turn > 1)? false : true;

        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
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

    
    //Called from dropdown list
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

    //Add card to deck if not included yet
    public bool AddCard(CardAttributes myCard)
    {
        if (!isCardExist(myCard)) //Adding card to deck
        {
            CardCounter.SetDeck(cards);
            CheckDeck();
            return true;

        }
        else
            return false;
    }

    //Go through all the cards in the deck to make sure the deck is full
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

    //Check if card already included in deck
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
        CardCounter.turns = 0;
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
