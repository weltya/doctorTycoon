using Scripts.Utils.Enum;
using System;
using UnityEngine;

public class NurseRoomData : Room
{
    public NurseData nurseData;
    private int exp =1;
    private int grs=1;
    public Transform point;
    public Boolean available = true;
    public EnumRoom typeRoom = EnumRoom.NurseRoom;
    public int exp_sub()
    {
        return exp;
    }
    public int guerison()
    {
        return grs;
    }
}