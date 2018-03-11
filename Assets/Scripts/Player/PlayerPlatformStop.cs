using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformStop : MonoBehaviour {

    //script zorgt er voor dat de player niet tegen een platform aan blijft plakken.

	public EigenInput EigenInput;
    public bool platformCollision = false; //voor movement in EigenInput

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformStop"))
        {
            //Debug.Log("contact");
            platformCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlatformStop"))
        {
            //Debug.Log("lost");
            platformCollision = false;
        }
    }
}
