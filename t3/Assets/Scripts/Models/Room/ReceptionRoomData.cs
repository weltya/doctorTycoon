using UnityEngine;
using System;
using Scripts.Utils.Enum;

public class ReceptionRoomData : Room
{
    private ReceptionRoomData reception;
    public Transform point;
    public int exp =1;
    public int grs =1;
    public Boolean available = true;
    public EnumRoom TypeRoom = EnumRoom.Reception;
    public int exp_sub()
    {
        return exp;
    }
    public int guerison()
    {
        return grs;
    }
}
