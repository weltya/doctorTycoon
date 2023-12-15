using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;

public class WaitingRoomData : Room
{
    public Dictionary<Transform,bool> points= new Dictionary<Transform,bool>();
    public int expSubjective;
    public int capacity;
    public int maxCapacity;
}