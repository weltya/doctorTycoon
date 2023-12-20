using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;

public class WaitingRoomData : Room
{
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