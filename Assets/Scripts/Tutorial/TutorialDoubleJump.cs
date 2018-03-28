using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDoubleJump : MonoBehaviour {

    //laat de text zien voor de doublejump tutorial.

    public Text doubleJump;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doubleJump.text = "Als je in de lucht bent kan je nog een keer Spatie in drukken om een tweede keer te springen";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doubleJump.text = "";
        }
    }
}

