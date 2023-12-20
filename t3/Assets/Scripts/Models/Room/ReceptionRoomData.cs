using UnityEngine;
using System;
using Scripts.Utils.Enum;

public class ReceptionRoomData : Room
{
    private ReceptionRoomData reception;
    public Transform point;
    public Boolean available = true;
    public EnumRoom TypeRoom = EnumRoom.Reception;
}
