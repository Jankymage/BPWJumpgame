using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EigenInput : MonoBehaviour
{
    [Range(0.01f,0.05f)]
    public float moveMulti = 1;
    public float jumpMulti = 6;
    public int jumpMax = 2;
    public Transform playerCam, character, viewPoint;
    public float mouseSpeed = 10f;
    private float Zoom = 2;
    private float ZoomSpeed = 2;
    private float ZoomMin = 2f;
    private float ZoomMax = -10f;

    private Rigidbody rb;
    private Vector3 movement;
    private float moveSpeedForward = 0;
    private float moveSpeedSide = 0;
    private float jumpSpeed = 0;
    private Collider coll;
    private int jumpTimes = 0;

    private float mouseY;
    private float mouseX;
    private float zoom = -3;
    private float zoomSpeed = 2;
    private float zoomMin = -2f;
    private float zoomMax = -10f;
    private float camYOffset = 1;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void Update()
    {

        //Voor het zoomen van de muis (met clamp)
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > zoomMin)
            zoom = zoomMin;
        if (zoom < zoomMax)
            zoom = zoomMax;
        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        if (Input.GetMouseButton(1)){
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * mouseSpeed;
            Cursor.visible = false;
        }
        else { Cursor.visible = true; }

        //Clampt de muis op boven en onderst as
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        //zorgt dat de camera naar de centerpoint blijft kijken
        playerCam.LookAt(viewPoint);
        //draait de camera afhankelijk van de muis stand
        viewPoint.localRotation = (Quaternion.Euler(mouseY, mouseX, 0));
        //zet de positie van de viewpoint afhankelijk van de positie van de character
        viewPoint.position = new Vector3(character.position.x, character.position.y + camYOffset, character.position.z);


        //reset de int voor het aantal sprongen, als de character op de grond is.
        if (Grounded()){
            jumpTimes = 0;
        }
        //zorgt dat de character vooruit loopt.
        if (Input.GetKey(KeyCode.W)){
            moveSpeedForward += moveMulti;
        } else if(Input.GetKey(KeyCode.S))
        {
            moveSpeedForward -= moveMulti;
        } else
        {
            moveSpeedForward = 0;
        }

        //zorgt dat de character vooruit loopt.
        if (Input.GetKey(KeyCode.A))
        {
            moveSpeedSide += moveMulti;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveSpeedSide -= moveMulti;
        }
        else
        {
            moveSpeedSide = 0;
        }

        //springt als op spatie gedrukt is en er nog spring "charges" over zijn.
        if (Input.GetButtonDown("Jump") && jumpTimes <= (jumpMax - 1)){
            jumpSpeed = jumpMulti;
            jumpTimes += 1;
        } else{
            jumpSpeed = 0;
        }

        character.rotation = Quaternion.Euler(0, viewPoint.eulerAngles.y, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //movement = (character.forward * moveSpeedForward) + (character.right * moveSpeedSide);

        movement = character.forward * moveSpeedForward;

        rb.MovePosition(movement + rb.position);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpSpeed, rb.velocity.z);
        
    }

    //Method that will look below character and see if there is a collider
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
    }
}
