using Scripts.Utils.Enum;
using System;
using UnityEngine;


/**
 * @class DoctorRoomData
 * @brief Represents a doctor room in the game.
 * @extends Room
 */
public class DoctorRoomData : Room
{
    public DoctorData Doctordata;
    public float  ExpSubjective = 1; //exp_sub
    public  float Health = 1; //guersion
    public Transform Point;
    public Boolean IsAvailable = true;
    public EnumRoom TypeRoom = EnumRoom.DoctorRoom;
    public float WaitTime = 1;
}