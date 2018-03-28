using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialView : MonoBehaviour {

    //laat de text zien voor de view tutorial.

    public Text view;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            view.text = "Als je de rechter muisknop ingedrukt houd kan je de camera en character draaien, met de scroll knop zoom je in en uit";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            view.text = "";
        }
    }
}
