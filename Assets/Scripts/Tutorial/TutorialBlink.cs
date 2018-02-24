using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBlink : MonoBehaviour {


    public Text Blink;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blink.text = "Als je in de lucht bent kan je op Shift drukken om vooruit te blinken";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blink.text = "";
        }
    }
}

