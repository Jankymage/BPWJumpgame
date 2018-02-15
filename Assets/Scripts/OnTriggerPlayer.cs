using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayer : MonoBehaviour {

    private int CollectLeft = 5;
    //public Text ColText;

    // Use this for initialization
    void Start () {
        //ColText = "";
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
                //ColText = "You Win!";
            }
        }
            
    }
}
