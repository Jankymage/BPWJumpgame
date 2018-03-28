using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMove : MonoBehaviour {

    //laat de text zien voor de movement tutorial.

    public Text move;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            move.text = "Gebruik WASD om het character te bewegen en Spatie om te springen";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            move.text = "";
        }
    }
}
