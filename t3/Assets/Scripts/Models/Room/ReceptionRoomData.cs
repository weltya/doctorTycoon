using UnityEngine;
using System;
using Scripts.Utils.Enum;

public class ReceptionRoomData : Room
{
    public ReceptionRoomData Reception;
    public Transform Point;
    public Boolean IsAvailable = true;
    public EnumRoom TypeRoom = EnumRoom.Reception;
    public float WaitTime = 1;
}
