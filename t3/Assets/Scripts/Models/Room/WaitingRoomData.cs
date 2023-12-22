using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;
using System;
using Scripts.Utils.Enum;

/**
 * @class WaitingRoomData
 * @brief Represents a waiting room in the game.
 * @extends Room
 */
public class WaitingRoomData : Room
{
    public float ExpSubjective=1;
    public float Health=1; // guerison
    public int Capacity;
    public int MaxCapacity;
    public List<PointData> ListPoint = new();
    public EnumRoom TypeRoom = EnumRoom.WaitingRoom;
}

/**
 * @struct PointData
 * @brief Represents a point in the waiting room where a patient can wait.
 */
[Serializable]
public class PointData
{
    public bool IsAvailable = true;
    public Transform Waypoint;
}