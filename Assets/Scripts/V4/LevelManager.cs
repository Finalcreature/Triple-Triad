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

    public void Start()
    {
       if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            CardCounter.rivaldeck = decks[0];
        }
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
