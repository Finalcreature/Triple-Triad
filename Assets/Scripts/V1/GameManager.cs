using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static Card selectedCard;
    public static List<Card> allCards = new List<Card>(); //Cards on board
    public static GridBehavior[] slots = new GridBehavior[9];

    

    private void Update()
    {
        slots = FindObjectsOfType<GridBehavior>();
        //foreach(Card card in allCards)
        //{
        //    print(allCards.Count);
        //}
    }
}

//[SerializeField] GameObject cardSlot;
//GridPart[,] gridParts = new GridPart[3, 3];
//Grid newGrid;

//// Start is called before the first frame update
//void Start()
//{
//    GameObject newCardSlot = Instantiate(cardSlot, new Vector2(0, 0), transform.rotation);
//    newCardSlot.name = "Card" + 0 + 0;
//    newCardSlot.AddComponent<Grid>();
//    newGrid = newCardSlot.GetComponent<Grid>();


//    for (int i = 0; i < 3; i++)
//    {
//        for (int j = 0; j < 3; j++)
//        {
//            GameObject newCardSlot = Instantiate(cardSlot, new Vector2(j, i), transform.rotation);
//            newCardSlot.name = "Card" + j + i;
//            newCardSlot.AddComponent<Grid>();
//            Grid newGrid = newCardSlot.GetComponent<Grid>();
//            gridParts[j, i] = newGrid.GetPart();

//        }
//    }




//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //foreach (var grid in gridParts)
//        //{
//        //    print(grid);
//        //    //print(gridParts[0, 0]);
//        //}
//    }
//}

//public class GridPart
//{
//    bool isFilled;
//    Vector3 pos;
//    public string name = "MyName";

//    public GridPart(Vector3 pos)
//    {
//        this.pos = pos;
//        isFilled = false;
//    }


//}

//public class Grid : MonoBehaviour
//{
//    GridPart gridPart;
//    public void Start()
//    {
//        gridPart = new GridPart(transform.position);
//       //print(gridPart);
//    }

//    public GridPart GetPart()
//    {
//        return gridPart;
//    }