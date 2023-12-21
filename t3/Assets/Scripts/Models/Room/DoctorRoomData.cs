using Scripts.Utils.Enum;
using System;
using UnityEngine;

public class DoctorRoomData : Room
{
    public DoctorData doctorData;
    public Transform point;
    public Boolean available = true;
    public EnumRoom typeRoom = EnumRoom.DoctorRoom;
}