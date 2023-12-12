using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;

public class WaitingRoomData : Room
{
    public List<PatientInfo> patients;
     public List<Transform> points;
     public int capacity;
     public int expSubjective;
}