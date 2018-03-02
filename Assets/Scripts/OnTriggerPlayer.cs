using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerPlayer : MonoBehaviour {

    //dit script zorgt er voor dat er bijgehouden word hoeveel objecten met de tag Block verzameld worden
    //als de in CollectLeft aangegeven hoeveelheid is verzameld, komt de tetxt "you win" in de aangegeven text box.

    public int CollectLeft = 6;
    public Text ColText;

    // Use this for initialization
    void Start () {
        ColText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            other.gameObject.SetActive(false);
            CollectLeft -= 1;
            if (CollectLeft <= 0)
            {
                ColText.text = "You Win!";
            }
        }
            
    }
}
