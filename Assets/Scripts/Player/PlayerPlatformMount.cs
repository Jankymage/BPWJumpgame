using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformMount : MonoBehaviour {


    //Script wat zorgt dat het character niet op en neer beweegt als het op een platform staat
    //http://johnstejskal.com/wp/how-to-stop-rigidbodies-sliding-and-falling-off-moving-platforms-in-unity3d-and-2d/

    private Transform m_currMovingPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            m_currMovingPlatform = other.gameObject.transform;
            transform.SetParent(m_currMovingPlatform);
            //Debug.Log("contact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            m_currMovingPlatform = null; 
            //Debug.Log("lost");
            transform.parent = null; 
        }
    }

    // void OnCollisionEnter(Collision coll)
    // {
    //     if (coll.gameObject.tag == "Platform")
    //     {
    //         m_currMovingPlatform = coll.gameObject.transform;
    //         transform.SetParent(m_currMovingPlatform);
    //     }
    // }
}
