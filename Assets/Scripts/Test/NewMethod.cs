using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMethod : MonoBehaviour
{
    [SerializeField] BoardPart[] slots;
    BoardPart[,] locs = new BoardPart[3, 3];

   

    void Start()
    {
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                locs[j, i] = slots[index];
                index++;
               
            }
            
        }

        
    }


    void Update()
    {
        print(locs[1, 1].GetCard());
    }
}
