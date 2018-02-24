using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDoubleJump : MonoBehaviour {


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

