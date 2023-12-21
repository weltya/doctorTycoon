using Scripts.Utils.Enum;
using System;
using UnityEngine;

public class DoctorRoomData : Room
{
    public DoctorData doctorData;
    public int  exp = 1; //exp_sub
    public  int grs = 1; //guersion
    public Transform point;
    public Boolean available = true;
    public EnumRoom typeRoom = EnumRoom.DoctorRoom;

    public int exp_sub()
    {
        return exp;
    }
    public int guerison()
    {
        return grs;
    }
}