using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;

public class WaitingRoomData : Room
{
    public Dictionary<Transform,bool> points= new Dictionary<Transform,bool>();
    public int expSubjective;
    public int capacity;
    public int maxCapacity;
    public List<PointData> ListPoint = new();
}

[Serializable]
public class PointData
{
    public bool IsAvailable = true;
    public Transform Waypoint;
}