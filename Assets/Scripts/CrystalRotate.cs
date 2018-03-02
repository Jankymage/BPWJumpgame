using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotate : MonoBehaviour {

    //plaats dit script op een object om het rond te laten draaien.

    void Update()
    {
        
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
       
    }
}
