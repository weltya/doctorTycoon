using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;
using Scripts.Utils.Enum;

public class WaitingRoomData : Room
{
    public float ExpSubjective=1;
    public float Health=1; // guerison
    public int Capacity;
    public int MaxCapacity;
    public List<PointData> ListPoint = new();
    public EnumRoom TypeRoom = EnumRoom.WaitingRoom;
    public float WaitTime = 0.5f;
}

[Serializable]
public class PointData
{
    public bool IsAvailable = true;
    public Transform Waypoint;
}