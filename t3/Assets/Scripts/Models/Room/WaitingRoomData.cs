using System.Collections.Generic;
using UnityEngine;
using Scripts.Models.Caracters;

public class WaitingRoomData : Room
{
    public List<PatientInfo> patients = new List<PatientInfo>();
    public List<Transform> points = new List<Transform>();
    
    public int expSubjective;
    public int capacity;
    public int maxCapacity;
}