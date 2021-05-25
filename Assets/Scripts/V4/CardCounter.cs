using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardCounter
{
    public static int playerCards = 5;
    public static int rivalCards = 5;
    public static bool isPlayerTurn = true;
    public static int turns = 1;
    public static CardAttributes[] cards;
    public static Pack rivaldeck;


    public static void SetDeck(CardAttributes[] myCards)
    {
        cards = myCards;
    }

    public static void SetRivalDeck(Pack deck)
    {
        rivaldeck = deck;
        Debug.Log(rivaldeck);
    }

}
