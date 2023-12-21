using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Scripts.Managers.BuilderManagers;



namespace Scripts.UII
{
    public class UIScoreManager : MonoBehaviour
    {
        private static UIScoreManager Instance;
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
        private int money=10000;
        private int exp_sub=0;
        private int guerison=0;
        [SerializeField] private ObjectsDatabaseSO database;
        private int _selectedObjectIndex;
        
        public static UIScoreManager GetInstance()
        {
            return Instance;
        }

        private void Awake()
        {
            Debug.Log("instance créer ");
             Instance=this;
        }
        
        
        

        /* public Score(ObjectsDatabaseSO database)
            {
                this.database = database;
            }
            */

        // Start is called before the first frame update
        private void Start()
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
            NurseCapacityText.text = "Infirmiéres : " + cap_nurse.ToString();
            }
        }

        public void UpdateDoctor(int cap_doctor)
        {
            if ( DoctorCapacityText!= null)
            {
                DoctorCapacityText.text = "Médcins : " + cap_doctor.ToString();
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
                ReceptionText.text = "Reception : " + cap_reception.ToString();
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
        public void addMoney(int montant){
            money=money+montant;
            UpdateMoney(money);
        }
    
    }
}
