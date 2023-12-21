using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Scripts.Managers.BuilderManagers;



namespace Scripts.UII
{
    public class UIScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ExperienceSubjectiveText ;
    public TextMeshProUGUI GuerisonText;
    public TextMeshProUGUI ReceptionText;
    public TextMeshProUGUI WaitingCapacityText;
    public TextMeshProUGUI NurseCapacityText;
    public TextMeshProUGUI PatientCapacityText;
    public TextMeshProUGUI DoctorCapacityText;
    public TextMeshProUGUI MoneyText;

    private int cap_patient;
    private int cap_nurse;
    private int cap_doctor;
    private int cap_waiting;
    private int cap_reception;
    private int money=0;
    private int exp_sub=0;
    private int guerison=0;
    private int patientsInNurse;
    private int patientsInDoctor;
    private int patientInReception;
    private string pd;
    private string pn;
    private string pr;
        [SerializeField] private ObjectsDatabaseSO database;
    private int _selectedObjectIndex;
    

     /* public Score(ObjectsDatabaseSO database)
        {
            this.database = database;
        }
        */



    // Start is called before the first frame update
    void Start()
    { 
    
        UpdateDoctor(0);
        Updatepatient(0);
        UpdateNurse(0);
        UpdateRoom(0);
        UpdateReception(0);
        UpdateMoney(10000);
        UpdateExp(0);
        UpdateGuerison(0);
      
    }

    public void Updatepatient(int cap_patient)
    {
        if (PatientCapacityText != null)
        {
            PatientCapacityText.text = "Patients : " + cap_patient.ToString();
        }
    }

    public void UpdateNurse(int cap_nurse)
    {
        if (NurseCapacityText != null)
        {
           NurseCapacityText.text = "Infirmiéres : "+ pn + " / " + cap_nurse.ToString();
        }
    }

    public void UpdateDoctor(int cap_doctor)
    {
        if ( DoctorCapacityText!= null)
        {
            DoctorCapacityText.text = "Médcins : " +  pd + "/ " + cap_doctor.ToString();
            //Debug.Log("UpdateDoctor called with cap_doctor: " + cap_doctor);
        }
    }

    public void UpdateRoom(int cap_waiting)
    {
        if(WaitingCapacityText!=null)
        {
            WaitingCapacityText.text= "Salle attente : "+ cap_waiting.ToString();
        }
    }

    public void UpdateReception(int cap_reception)
    {
        if(ReceptionText !=null)
        {
            ReceptionText.text = "Reception : "+ pr + "/ " + cap_reception.ToString();
        }
    }

    public void UpdateMoney(int money)
    {
        if(MoneyText!= null)
        {
            MoneyText.text= "Argent : " + money.ToString();
        }
        this.money = money;
    }

    public void UpdateGuerison(int guerison)
    {
        if (GuerisonText != null)
        {
            GuerisonText.text = "Guérison : " + guerison.ToString();
          
        }
    }
    

    public void UpdateExp(int exp_sub)
    {
        if (ExperienceSubjectiveText != null)
        {
           ExperienceSubjectiveText.text = "Experience Subjective : " + exp_sub.ToString();

        }
    }

    public void UpdatePatientInNurse(int patientsInNurse)
    {
       this.pn=  patientsInNurse.ToString();
      
    }
    public string getPatientsInNurse()
    {
        return pn;
    }

    public void UpdatePatientInDoctor(int patientsInDoctor)
    {
        this.pd= patientsInDoctor.ToString();
       
    }
    public string getPatientInDoctor()
    {
        return pd;
    }
    public void UpdatePatientInReception(int patientInReception )
    {
        this.pr=patientInReception.ToString();
    }
    public string getPatientInReception()
    {
        return pr;
    }

public bool canBuy(int ID)
{
 int prix= database.objectsData[ID].Prix;
    if(prix > money)
    {
        return false;
    }
    money-=prix;
    UpdateMoney(money);
    return true;
    
}
  
}

}
