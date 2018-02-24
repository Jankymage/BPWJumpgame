using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGoal : MonoBehaviour {

    public Text goal;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goal.text = "Verzamel alle kristallen op de platformen!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goal.text = "";
        }
    }
}
