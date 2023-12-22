using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;
using Scripts.Utils.Enum;

public class WaitingRoomData : Room
{
    public int exp=1;
    public int grs=1; // guerison
    public int capacity;
    public int maxCapacity;
    public List<PointData> ListPoint = new();
    public EnumRoom typeRoom = EnumRoom.WaitingRoom;

    public int exp_sub()
    {
        return exp;
    }
    public int guerison()
    {
        return grs;
    }
}

[Serializable]
public class PointData
{
    public bool IsAvailable = true;
    public Transform Waypoint;
}