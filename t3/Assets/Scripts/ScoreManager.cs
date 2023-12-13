using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI roomCapText;
    public TextMeshProUGUI patientCapText;
    public TextMeshProUGUI docCapText;
    public TextMeshProUGUI nurseCapText;
    public TextMeshProUGUI guerisonText;
    public TextMeshProUGUI expSubText;

    // Start is called before the first frame update
    void Start()
    { 
        UpdateRoomCapLabel(calculate_room_cap());
        UpdatepatientCapLabel(calculate_patient_cap());
        UpdateNurseCapLabel(calculate_nurse_cap());
        UpdateDoctorCapLabel(calculate_doctor_cap());
        UpdateGuerisonLabel(calculate_guersion());
        UpdateExpLabel(calculate_exp_sub());
        // Add other initialization code...
    }

   
    void UpdateRoomCapLabel(int cap_roomwait)
    {
        if (roomCapText != null)
        {
            roomCapText.text = cap_roomwait.ToString();
        }
    }

    void UpdatepatientCapLabel(int cap_patientwait)
    {
        if (patientCapText != null)
        {
            patientCapText.text = cap_patientwait.ToString();
        }
    }

    void UpdateNurseCapLabel(int cap_nurse)
    {
        if (nurseCapText != null)
        {
            nurseCapText.text = cap_nurse.ToString();
        }
    }

    void UpdateDoctorCapLabel(int cap_doctor)
    {
        if (docCapText != null)
        {
            docCapText.text = cap_doctor.ToString();
        }
    }

    void UpdateGuerisonLabel(int guerison)
    {
        if (guerisonText != null)
        {
            guerisonText.text = guerison.ToString();
        }
    }

    void UpdateExpLabel(int exp_sub)
    {
        if (expSubText != null)
        {
            expSubText.text = exp_sub.ToString();
        }
    }

    int calculate_room_cap()  // calculate_room_cap(roomdata)
    {
        return 10;
    }

    int calculate_patient_cap() //calculate_nurse_cap(nursedata)
    {
        return 10;
    }

    int calculate_nurse_cap() // calculate_patient_cap(patientdata)
    {
        return 10;
    }

    int calculate_doctor_cap() // calculate_doctor_cap(doctordata)
    {
        return 10;
    }

    int calculate_guersion()
    {
        return 10;
    }

    int calculate_exp_sub()
    {
        return 10;
    }
}