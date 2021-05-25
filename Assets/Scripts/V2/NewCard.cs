using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCard : MonoBehaviour
{
    public enum Owner { Player,Opponent};
    public Owner owner { get; set; }

    public int[] nums = new int[4];

    public NewCard(Owner owner,int[] nums)
    {
        this.owner = owner;
        this.nums = nums;
    }

}
