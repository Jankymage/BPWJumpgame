using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derp : MonoBehaviour {

    //Trasforms
    public Transform playerCam, character, centerPoint;

    //character controller declaration
    CharacterController player;

    //Mouse Rotation
    private float rotX, rotY;

    //Mouse Y Poxsition
    public float mouseYPosition = 1f;

    //Mouse Sensitivity
    public float Sensitivity = 10f;

    //Mouse Zoom
    private float zoom;
    public float zoomSpeed = 2;

    //Clamping Zoom
    public float zoomMin = -2f;
    public float zoomMax = -10f;

    public float rotationSpeed = 5f;

    //Move Front Back left & Right
    private float moveFB, moveLR;

    //Movement Speed
    public float Speed = 2f;

    //Velocity of Gravity
    public float verticalVelocity;

    //Jump Distance
    public float jumpDist = 5f;

    //Multiple Jumps
    int jumpTimes;

    // Use this for initialization
    void Start()
    {
        //character controller
        player = GameObject.Find("Player").GetComponent<CharacterController>();

        //mouse zoom
        zoom = -3;

    }

    // Update is called once per frame

    void Update()
    {

        //Mouse Zoom Input
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > zoomMin)
            zoom = zoomMin;
        if (zoom < zoomMax)
            zoom = zoomMax;

        //Mouse Camera Input
        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        //Mouse Rotation
        if (Input.GetMouseButton(1))
        {
            rotX += Input.GetAxis("Mouse X") * Sensitivity;
            rotY -= Input.GetAxis("Mouse Y") * Sensitivity;

        }

        //Clamp Camera
        rotY = Mathf.Clamp(rotY, -60f, 60f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(rotY, rotX, 0);

        //Movement Speed
        moveFB = Input.GetAxis("Vertical") * Speed;
        moveLR = Input.GetAxis("Horizontal") * Speed;

        //Movement Direction
        Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);

        //Movement Rotation
        movement = character.rotation * movement;

        player.Move(movement * Time.deltaTime);

        centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        //Movement Input
        if (Input.GetAxis("Horizontal") > 0 | Input.GetAxis("Horizontal") < 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

        //** Note, go into Edit -> Project settings -> Input and ADD 'Jump" ass 'space" **
        //check if player is on the ground
        if (player.isGrounded == true)
        {
            //set jump times to 0
            jumpTimes = 0;
        }

        if (jumpTimes < 2)
        {
            //Jump Input
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity += jumpDist;

                //set jumptimes +1
                jumpTimes += 1;
            }
        }
    }

    // FixedUpdate is called once every other frame

    void FixedUpdate()
    {
        //check if player is not on the ground
        if (player.isGrounded == false)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        //check if the player is on the ground
        else
        {
            verticalVelocity = 0f;
        }
    }
}
