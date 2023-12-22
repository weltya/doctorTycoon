using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Scripts.Managers.BuilderManagers;
using UnityEngine.UI;

namespace Scripts.UII
{
    public class UIScoreManager : MonoBehaviour
{
        public TextMeshProUGUI ExperienceSubjectiveText ;
        public TextMeshProUGUI GuerisonText;
        public TextMeshProUGUI ReceptionText;
        public TextMeshProUGUI WaitingCapacityText;
        public TextMeshProUGUI NurseCapacityText;
        public TextMeshProUGUI DoctorCapacityText;
        public TextMeshProUGUI MoneyText;

        public Image ExperienceSubjective;
        public Image GuerisonPanel;
        public Image ReceptionPanelImage;
        public Image WaitingPanelImage;
        public Image NursePanelImage;
        public Image DoctorPanelImage;

        private int money=10000;
        private int exp_sub=0;
        private int guerison=0;

        private int _nbPatientDoctor = 0;
        private int _nbPatientNurse = 0;
        private int _nbPatientReception = 0;
        private int _nbPatientWaiting = 0;

        private int _nbDoctorRoom = 0;
        private int _nbNurseRoom = 0;
        private int _nbWaitingRoom = 0;
        private int _nbReceptionRoom = 0;

        private Color32 _colorGreen = new Color32(146, 185, 32, 255);
        private Color32 _colorOrange = new Color32(222, 169, 29, 255);
        private Color32 _colorRed = new Color32(221, 99, 26, 255);

        [SerializeField] private ObjectsDatabaseSO database;


            private void Awake()
            {
                Debug.Log("instance uiscoremanager créer ");
            }

        private void Update()
        {
            ReceptionPanelImage.color = Color.red;
        }
        private void Start()
        { 
            UpdateTextDoctor();
            UpdateTextNurse();
            UpdateTextWaiting();
            UpdateTextReception();
            UpdateMoney(money);
            UpdateExp(exp_sub);
            UpdateGuerison();
            ReceptionPanelImage.color = _colorRed;
        }
        /*update nb patient in different room*/
        public void UpdateNbPatientNurse(int nbPatient)
        {
            _nbPatientNurse = nbPatient;
            UpdateTextNurse();
        }

        public void UpdateNbPatientDoctor(int nbPatient)
        {
            _nbPatientDoctor = nbPatient;
            UpdateTextDoctor();
        }

        public void UpdateNbPatientReception(int nbPatient)
        {
            _nbPatientReception = nbPatient;
            UpdateTextReception();
        }

        public void UpdateNbPatientWaiting(int nbPatient)
        {
            _nbPatientWaiting = nbPatient;
            UpdateTextWaiting();
        }

        /*update nb patient in different room*/
        public void UpdateNbRoomNurse(int nbPatient)
        {
            _nbNurseRoom = nbPatient;
            UpdateTextNurse();
        }

        public void UpdateNbRoomDoctor(int nbPatient)
        {
            _nbDoctorRoom = nbPatient;
            UpdateTextDoctor();
        }

        public void UpdateNbRoomReception(int nbPatient)
        {
            _nbReceptionRoom = nbPatient;
            UpdateTextReception();
        }

        public void UpdateNbRoomWaiting(int nbPatient)
        {
            _nbWaitingRoom = nbPatient;
            UpdateTextWaiting();
        }

        /*update textmesh - capacity*/
        public void UpdateTextNurse()
        {
            if (NurseCapacityText != null)
            {
               NurseCapacityText.text = "Infirmières : " + _nbPatientNurse + "/" + _nbNurseRoom;
            }
        }

        public void UpdateTextDoctor()
        {
            if ( DoctorCapacityText!= null)
            {
                DoctorCapacityText.text = "Médecins : " +  _nbPatientDoctor + "/" + _nbDoctorRoom;
            }
        }

        public void UpdateTextWaiting()
        {
            if(WaitingCapacityText!=null)
            {
                WaitingCapacityText.text= "Salles attente : " + _nbPatientWaiting + "/" + _nbWaitingRoom;
                WaitingCapacityText.text = "rien";
            }
        }

        public void UpdateTextReception()
        {
            if(ReceptionText != null)
            {
                ReceptionText.text = "Réception : " + _nbPatientReception + "/" + _nbReceptionRoom;
            }
        }









        /*update textmesh health-money*/
        public void UpdateMoney(int money)
        {
            if(MoneyText!= null)
            {
                MoneyText.text= "Argent : " + money.ToString();
            }
            this.money = money;
        }

        public void setGuerison(int g)
        {
            this.guerison= guerison+g;
            UpdateGuerison();
        }

        public void UpdateGuerison()
        {
            if (GuerisonText != null)
            {
                GuerisonText.text = "Guérison : " + guerison.ToString();
          
            }
        }
    
        public void setExp(int e)
        {
           this.exp_sub= exp_sub+e;
           UpdateExp(exp_sub);
       
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
