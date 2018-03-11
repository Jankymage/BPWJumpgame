using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveForward : MonoBehaviour {

	//zorgt er voor dat een object naar voor en achter beweegt.
    //maximale vooruitgang word aangegeven met maxMove
    //moveSpeed bepaald hoe snel het object beweegt.

    public float moveMax = 10;
    [Range(1f, 7f)]
    public float moveSpeed = 1;

    private float moveTimer = 0;
    private bool moveBool = false;

		
	//Lateupdate omdat anders character controlls overschreven worden
	void LateUpdate () {

        moveTimer += 1 * Time.deltaTime * moveSpeed;

        if (moveTimer >= moveMax)
        {
            moveTimer = 0;

            if (moveBool)
                moveBool = false;
            else moveBool = true;

        }

        if (!moveBool)
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        else transform.Translate(Vector3.forward * -Time.deltaTime * moveSpeed);

    }
}
