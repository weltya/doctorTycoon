using TMPro;
using UnityEngine;

public class Infos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel;

    public void printReceptionInfos()
    {
        panel.SetActive(true);
        title.SetText("Salle de r�ception");
        text.SetText("- Temps d'attente : 3s \n - Prix : 750 \n - Etat de la salle : propre");
    }

    public void printWaitingRoom1Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'attente 1");
        text.SetText("- Prix : 1000 \n - Etat de la salle : Pas de distributeur, toilettes �loign�es, si�ges peu confortables \n");
    }

    public void printWaitingRoom2Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'attente 2");
        text.SetText("- Prix : 1500 \n - Etat de la salle : Distributeurs, toilettes pr�sentes dans la salle, si�ges confortables \n");
    }

    public void printNurseRoom1Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'infirmi�re 1");
        text.SetText("- Temps d'attente : 3s \n - Prix : 1000 \n - Etat de la salle : Petite salle peu accueillante et assez laide. L'infirmi�re ne prend pas en compte l'exp�rience subjective");

    }

    public void printNurseRoom2Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'infirmi�re 2");
        text.SetText("- Temps d'attente : 7s \n - Prix : 1500 \n - Etat de la salle : Petite salle peu accueillante et assez laide. L'infirmi�re prend en compte l'exp�rience subjective");
    }


    public void printNurseRoom3Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'infirmi�re 3");
        text.SetText("- Temps d'attente : 3s \n - Prix : 1500 \n - Etat de la salle : Grande salle accueillante et assez belle. L'infirmi�re ne prend pas en compte l'exp�rience subjective");
    }

    public void printNurseRoom4Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle d'infirmi�re 4");
        text.SetText("- Temps d'attente : 7s \n - Prix : 2000 \n- Etat de la salle : Grande salle accueillante et assez belle. L'infirmi�re prend en compte l'exp�rience subjective");
    }

    public void printDoctorRoom1Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle de docteur 1");
        text.SetText("- Temps d'attente : 5s \n - Prix : 1500 \n - Etat de la salle : Petite salle peu accueillante et assez laide. Le docteur ne prend pas en compte l'exp�rience subjective");
    }

    public void printDoctorRoom2Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle de docteur 2");
        text.SetText("- Temps d'attente : 10s \n - Prix : 2000 \n- Etat de la salle : Petite salle peu accueillante et assez laide. Le docteur prend en compte l'exp�rience subjective");
    }

    public void printDoctorRoom3Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle de docteur 3");
        text.SetText("- Temps d'attente : 5s \n - Prix : 2000 \n - Etat de la salle : Grande salle accueillante et assez belle. Le docteur ne prend pas en compte l'exp�rience subjective");
    }

    public void printDoctorRoom4Infos()
    {
        panel.SetActive(true);
        title.SetText("Salle de docteur 4");
        text.SetText("- Temps d'attente : 10s \n - Prix : 2500 \n - Etat de la salle : Grande salle accueillante et assez belle. Le docteur prend en compte l'exp�rience subjective");
    }

    public void hidePopup()
    {
        panel.SetActive(false);
    }
}