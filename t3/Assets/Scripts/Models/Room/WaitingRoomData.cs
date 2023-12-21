using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;
using Scripts.Utils.Enum;

public class WaitingRoomData : Room
{
    public int expSubjective;
    public int capacity;
    public int maxCapacity;
    public List<PointData> ListPoint = new();
    public EnumRoom typeRoom = EnumRoom.WaitingRoom;
}

[Serializable]
public class PointData
{
    public bool IsAvailable = true;
    public Transform Waypoint;
}