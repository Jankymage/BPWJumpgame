using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {

    //zorgt er voor dat een object op en neer beweegt.
    //maximale hoogte word aangegeven met maxMove
    //moveSpeed bepaald hoe snel het object beweegt.

    public float moveMax = 10;
    [Range(1f, 7f)]
    public float moveSpeed = 1;

    private float moveTimer = 0;
    private bool moveBool = false;

		
	// Update is called once per frame
	void Update () {

        moveTimer += 1 * Time.deltaTime * moveSpeed;

        if (moveTimer >= moveMax)
        {
            moveTimer = 0;

            if (moveBool)
                moveBool = false;
            else moveBool = true;

        }

        if (!moveBool)
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        else transform.Translate(Vector3.up * -Time.deltaTime * moveSpeed);

    }
}
