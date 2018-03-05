using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformMount : MonoBehaviour {


    //WIP
    //http://johnstejskal.com/wp/how-to-stop-rigidbodies-sliding-and-falling-off-moving-platforms-in-unity3d-and-2d/


    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            m_currMovingPlatform = coll.gameObject.transform;
            transform.SetParent(m_currMovingPlatform);
        }
    }
}
