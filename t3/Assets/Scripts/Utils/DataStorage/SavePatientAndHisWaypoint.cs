using Scripts.Gameplay.Caracters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class SavePatientAndHisWaypoint
 * @brief Represents data for saving the state of a patient and related room information.
 */
public class SavePatientAndHisWaypoint
{
    public PatientGameplay PatientGameplay;
    public ReceptionRoomData ReceptionRoomData;
    public WaitingRoomData WaitingRoomData;
    public PointData PointDataWaitingNurse;
    public NurseRoomData NurseRoomData;
    public WaitingRoomData WaitingRoomData2;
    public PointData PointDataWaitingNurse2;
    public DoctorRoomData DoctorRoomData;

}
