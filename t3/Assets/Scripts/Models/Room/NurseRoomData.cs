using Scripts.Utils.Enum;
using System;
using UnityEngine;

public class NurseRoomData : Room
{
    public NurseData nurseData;
    public Transform point;
    public Boolean available = true;
    public EnumRoom typeRoom = EnumRoom.NurseRoom;
}