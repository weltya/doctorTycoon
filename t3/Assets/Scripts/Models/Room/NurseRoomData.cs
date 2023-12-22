using Scripts.Utils.Enum;
using System;
using UnityEngine;


/**
 * @class NurseRoomData
 * @brief Represents a nurse room in the game.
 * @extends Room
 */
public class NurseRoomData : Room
{
    public NurseData NurseData;
    public float ExpSubjective = 1;
    public float Health=1;
    public Transform Point;
    public Boolean IsAvailable = true;
    public EnumRoom TypeRoom = EnumRoom.NurseRoom;
    public float WaitTime = 1;
}