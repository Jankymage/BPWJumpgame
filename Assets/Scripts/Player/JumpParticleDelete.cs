using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticleDelete : MonoBehaviour {

    //zorgt er voor dat de jump partice effect objecten verwijderd worden
    //word ook gebruikt voor CrystalSound, BlinkSound en JumpSound objecten.

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5);
    }
}
