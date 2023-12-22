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

        private int _money=10000;
        private float _expSubjective=0;
        private float _health=0;

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

        private void UpdateColor()
        {
            //Reception
            if(_nbPatientReception < _nbReceptionRoom * 5)
                ReceptionPanelImage.color = _colorGreen;
            else if(_nbPatientReception >= _nbReceptionRoom * 15)
                ReceptionPanelImage.color = _colorRed;
            else
                ReceptionPanelImage.color = _colorOrange;

            //Nurse
            if (_nbPatientNurse < _nbNurseRoom * 10)
                NursePanelImage.color = _colorGreen;
            else if (_nbPatientNurse >= _nbWaitingRoom * 15)
                NursePanelImage.color = _colorRed;
            else
                NursePanelImage.color = _colorOrange;

            //Doctor
            if (_nbPatientDoctor < _nbDoctorRoom * 10)
                DoctorPanelImage.color = _colorGreen;
            else if (_nbPatientDoctor >= _nbDoctorRoom * 15)
                DoctorPanelImage.color = _colorRed;
            else
                DoctorPanelImage.color = _colorOrange;

            //Waiting
            if (_nbWaitingRoom != 0) 
            {
                if (( (float)_nbPatientWaiting / (float)_nbWaitingRoom ) < 0.3f)
                    WaitingPanelImage.color = _colorGreen;
                else if (( (float)_nbPatientWaiting / (float)_nbWaitingRoom) < 0.6f)
                    WaitingPanelImage.color = _colorOrange;
                else
                    WaitingPanelImage.color = _colorRed;
            }

            //Exp subjective
            if (_expSubjective > 20f)
                ExperienceSubjective.color = _colorGreen;
            else if (_expSubjective < -20f)
                ExperienceSubjective.color = _colorRed;
            else
                ExperienceSubjective.color = _colorOrange;

            //health
            if (_health > 20f)
                GuerisonPanel.color = _colorGreen;
            else if (_health < -20f)
                GuerisonPanel.color = _colorRed;
            else
                GuerisonPanel.color = _colorOrange;

        }
        private void Start()
        { 
            UpdateTextDoctor();
            UpdateTextNurse();
            UpdateTextWaiting();
            UpdateTextReception();
            UpdateMoney(_money);
            UpdateExp(_expSubjective);
            UpdateGuerison();
            UpdateColor();
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

        /*update nb room*/
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

        public void UpdateNbRoomWaiting(int maxCapacity)
        {
            _nbWaitingRoom = maxCapacity;
            UpdateTextWaiting();
        }

        /*update textmesh - capacity*/
        private void UpdateTextNurse()
        {
            if (NurseCapacityText != null)
            {
               NurseCapacityText.text = "Infirmières : " + _nbPatientNurse + "/" + _nbNurseRoom;
            }
            UpdateColor();
        }

        private void UpdateTextDoctor()
        {
            if ( DoctorCapacityText!= null)
            {
                DoctorCapacityText.text = "Médecins : " +  _nbPatientDoctor + "/" + _nbDoctorRoom;
            }
            UpdateColor();
        }

        private void UpdateTextWaiting()
        {
            if(WaitingCapacityText!=null)
            {
                WaitingCapacityText.text= "Salles attente : " + _nbPatientWaiting + "/" + _nbWaitingRoom;
            }
            UpdateColor();
        }

        private void UpdateTextReception()
        {
            if(ReceptionText != null)
            {
                ReceptionText.text = "Réception : " + _nbPatientReception + "/" + _nbReceptionRoom;
            }
            UpdateColor();
        }









        /*update textmesh health-money*/
        public void UpdateMoney(int money)
        {
            if(MoneyText!= null)
            {
                MoneyText.text= "Argent : " + money.ToString();
            }
            this._money = money;
        }

        public void setGuerison(float g)
        {
            this._health= _health+g;
            UpdateGuerison();
        }

        public void UpdateGuerison()
        {
            if (GuerisonText != null)
            {
                GuerisonText.text = "Guérison : " + _health.ToString();
          
            }
        }
    
        public void setExp(float e)
        {
           this._expSubjective= _expSubjective+e;
           UpdateExp(_expSubjective);
       
        }

        public void UpdateExp(float expSub)
        {
            if (ExperienceSubjectiveText != null)
            {
               ExperienceSubjectiveText.text = "Experience Subjective : " + expSub.ToString();
            }
        }

        

        public bool canBuy(int ID)
        {
        int prix= database.objectsData[ID].Prix;
            if(prix > _money)
            {
                return false;
            }
            _money-=prix;
            UpdateMoney(_money);
            return true;
        
        }
        public void addMoney(int montant){
                    _money=_money+montant;
                    UpdateMoney(_money);
        }
    
    }

}
