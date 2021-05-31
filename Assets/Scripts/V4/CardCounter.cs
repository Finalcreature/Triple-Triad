using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardCounter
{
    public static int playerCards = 5; //Amount of cards owned by player
    public static int rivalCards = 5; //Amount of cards owned by rival
    public static bool isPlayerTurn = true; 
    public static int turns = 1; //Amount of turns to determine if the board is full
    public static CardAttributes[] cards; //Player's deck
    public static Pack rivaldeck; //Rival's deck
   

    //The cards I set manually in the LevelManager will get added here (update with each drag/press of a card in deck
    public static void SetDeck(CardAttributes[] myCards)
    {
        cards = myCards;
    }

    public static void SetRivalDeck(Pack deck)
    {
        rivaldeck = deck;
    }

    public static void SetExistingDeck(CardAttributes[] myCards)
    {
        myCards = cards;
    }

}
