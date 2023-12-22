using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Scripts.Managers.BuilderManagers;
using UnityEngine.UI;

namespace Scripts.UII
{
     /**
    * @class UIScoreManager
    * @brief Manages the user interface elements related to scores and resources in the game.
    */
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
        /**
        * @brief Updates the visual representation based on game statistics.
        */
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
        /**
        * @brief Start is called before the first frame update.
        * Initializes the UI with initial values.
        */
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
        /**
        * @brief Updates the number of patients in the nurse room.
        * @param nbPatient The new number of patients.
        */
        public void UpdateNbPatientNurse(int nbPatient)
        {
            _nbPatientNurse = nbPatient;
            UpdateTextNurse();
        }
        /**
        * @brief Updates the number of patients in the doctor room.
        * @param nbPatient The new number of patients.
        */
        public void UpdateNbPatientDoctor(int nbPatient)
        {
            _nbPatientDoctor = nbPatient;
            UpdateTextDoctor();
        }

        /**
        * @brief Updates the number of patients in the reception.
        * @param nbPatient The new number of patients.
        */
        public void UpdateNbPatientReception(int nbPatient)
        {
            _nbPatientReception = nbPatient;
            UpdateTextReception();
        }

        /**
        * @brief Updates the number of patients in the waiting room.
        * @param nbPatient The new number of patients.
        */
        public void UpdateNbPatientWaiting(int nbPatient)
        {
            _nbPatientWaiting = nbPatient;
            UpdateTextWaiting();
        }

        /**
        * @brief Updates the number of nurse rooms.
        * @param nbPatient The new number of nurse rooms.
        */
        public void UpdateNbRoomNurse(int nbPatient)
        {
            _nbNurseRoom = nbPatient;
            UpdateTextNurse();
        }

        /**
        * @brief Updates the number of doctor rooms.
        * @param nbPatient The new number of doctor rooms.
        */
        public void UpdateNbRoomDoctor(int nbPatient)
        {
            _nbDoctorRoom = nbPatient;
            UpdateTextDoctor();
        }

        /**
        * @brief Updates the number of reception rooms.
        * @param nbPatient The new number of reception rooms.
        */
        public void UpdateNbRoomReception(int nbPatient)
        {
            _nbReceptionRoom = nbPatient;
            UpdateTextReception();
        }

        /**
        * @brief Updates the number of waiting rooms.
        * @param maxCapacity The new maximum capacity of the waiting rooms.
        */
        public void UpdateNbRoomWaiting(int maxCapacity)
        {
            _nbWaitingRoom = maxCapacity;
            UpdateTextWaiting();
        }

        /**
        * @brief Updates the text for nurse room capacity and adjusts visual representation.
        */
        private void UpdateTextNurse()
        {
            if (NurseCapacityText != null)
            {
               NurseCapacityText.text = "Infirmières : " + _nbPatientNurse + "/" + _nbNurseRoom;
            }
            UpdateColor();
        }

         /**
        * @brief Updates the text for doctor room capacity and adjusts visual representation.
        */
        private void UpdateTextDoctor()
        {
            if ( DoctorCapacityText!= null)
            {
                DoctorCapacityText.text = "Médecins : " +  _nbPatientDoctor + "/" + _nbDoctorRoom;
            }
            UpdateColor();
        }

        /**
        * @brief Updates the text for waiting room capacity and adjusts visual representation.
        */
        private void UpdateTextWaiting()
        {
            if(WaitingCapacityText!=null)
            {
                WaitingCapacityText.text= "Salles attente : " + _nbPatientWaiting + "/" + _nbWaitingRoom;
            }
            UpdateColor();
        }

        /**
        * @brief Updates the text for reception capacity and adjusts visual representation.
        */
        private void UpdateTextReception()
        {
            if(ReceptionText != null)
            {
                ReceptionText.text = "Réception : " + _nbPatientReception + "/" + _nbReceptionRoom;
            }
            UpdateColor();
        }

         /**
        * @brief Updates the text for in-game currency and adjusts visual representation.
        * @param money The new amount of in-game currency.
        */
        public void UpdateMoney(int money)
        {
            if(MoneyText!= null)
            {
                MoneyText.text= "Argent : " + money.ToString();
            }
            this._money = money;
        }

        /**
        * @brief Increases the healing statistics and updates the corresponding text.
        * @param g The amount to increase the healing statistics.
        */
        public void setGuerison(float g)
        {
            this._health= _health+g;
            UpdateGuerison();
        }

        /**
        * @brief Updates the text for healing statistics.
        */
        public void UpdateGuerison()
        {
            if (GuerisonText != null)
            {
                GuerisonText.text = "Guérison : " + _health.ToString();
          
            }
        }
        /**
        * @brief Increases the subjective experience and updates the corresponding text.
        * @param e The amount to increase the subjective experience.
        */ 
        public void setExp(float e)
        {
           this._expSubjective= _expSubjective+e;
           UpdateExp(_expSubjective);
       
        }

        /**
        * @brief Updates the text for subjective experience.
        * @param expSub The new value of subjective experience.
        */
        public void UpdateExp(float expSub)
        {
            if (ExperienceSubjectiveText != null)
            {
               ExperienceSubjectiveText.text = "Experience Subjective : " + expSub.ToString();
            }
        }

        /**
        * @brief Checks if the player can buy a certain object based on its ID and cost.
        * @param ID The ID of the object to be bought.
        * @return True if the player can afford the object, false otherwise.
        */
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

        /**
        * @brief Adds in-game currency to the player's balance.
        * @param montant The amount of in-game currency to add.
        */
        public void addMoney(int montant){
                    _money=_money+montant;
                    UpdateMoney(_money);
        }
    
    }

}
