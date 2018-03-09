using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EigenInput : MonoBehaviour
{
    //NOG  VOORZIEN VAN COMMENTS!!!!
    //NOG  VOORZIEN VAN COMMENTS!!!!
    //NOG  VOORZIEN VAN COMMENTS!!!!
    //NOG  VOORZIEN VAN COMMENTS!!!!

    [Range(0.1f,0.5f)]
    public float moveMulti;
    public float jumpSpeed = 6;
    public int jumpMax = 2;
    public Transform playerCam, character, viewPoint;
    public float mouseSpeed = 10f;
    public int dashMax = 1;
    [Range(5f, 20f)]
    public float dashDistance;
    [Range(0f, 20f)]
    public float gravMulti;

    private float Zoom = 2;
    private float ZoomSpeed = 2;
    private float ZoomMin = 2f;
    private float ZoomMax = -10f;

    private Rigidbody rb;
    private float moveSpeedForward = 0;
    private float moveSpeedSide = 0;
    private CapsuleCollider coll;
    private int jumpTimes = 0;
    public Vector3 movement; //public zetten om met een ander script er bij te kunnen.
    private bool jumpBool; 
    public GameObject jumpParticles;

    private float mouseY;
    private float mouseX;
    private float zoom = -3;
    private float zoomSpeed = 2;
    private float zoomMin = -2f;
    private float zoomMax = -10f;
    private float camYOffset = 1;

    private int dashTimes = 0;
    private bool dashPossible;
    private bool dashBool;

    //TODO
    //separate scripts...
    //comments
    //platforms vervangen
    //zorgen dat character niet tegen platforms blijft plakken.
    //side moving platforms?

   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
    }

    void Update()
    {

        //Voor het zoomen van de muis (met clamp)
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom = Mathf.Clamp(zoom, zoomMax, zoomMin);

        //if (zoom > zoomMin)
        //    zoom = zoomMin;
        //if (zoom < zoomMax)
        //    zoom = zoomMax;
        playerCam.transform.localPosition = new Vector3(0, 0, zoom);

        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
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

        //springt als op spatie gedrukt is en er nog spring "charges" over zijn.
        if (Input.GetButtonDown("Jump") && jumpTimes <= (jumpMax - 2))
        {
            jumpBool = true;
            jumpTimes += 1;
            Instantiate(jumpParticles, character);
        }
        
        if (Input.GetButtonDown("Fire3") && dashPossible && dashTimes <= dashMax -1)
        {
            dashBool = true;
            dashTimes += 1;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //jumpText.text = "Jumps = " + jumpTimes.ToString();



        //reset de int voor het aantal sprongen, als de character op de grond is.
        if (Grounded())
        {
            jumpTimes = 0;
            dashPossible = false;
            dashTimes = 0;
        }
        else
        {
            dashPossible = true;
        }

        //zorgt dat de character vooruit loopt.
        if (Input.GetKey(KeyCode.W))
        {
            moveSpeedForward = moveMulti;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveSpeedForward = -moveMulti;
        }
        else
        {
            moveSpeedForward = 0;
        }

        //zorgt dat de character vooruit loopt.
        if (Input.GetKey(KeyCode.A))
        {
            moveSpeedSide = -moveMulti;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveSpeedSide = moveMulti;
        }
        else
        {
            moveSpeedSide = 0;
        }

        Vector3 dir = playerCam.forward;
        
        if (dashBool)
        {
            rb.MovePosition((character.forward * dashDistance) + rb.position);
            
            dashBool = false;
        }

        character.rotation = Quaternion.Euler(0, viewPoint.eulerAngles.y, 0);

        movement = (character.forward * moveSpeedForward) + (character.right * moveSpeedSide);


        //checked waar heen beweegt of hij mag bewegen.
        //Vector3 buttom = coll.center - Vector3.down * 0.9f;
        //Vector3 top = coll.center - Vector3.up;
        //if (Physics.CapsuleCast(buttom, top, 0.5f, movement.normalized, movement.magnitude) == false)
        //{
            rb.MovePosition(movement + rb.position);
        //}

        if (jumpBool)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            //rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            jumpBool = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravMulti - 1) * Time.deltaTime;
        }



    }

    //Method that will look below character and see if there is a collider
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
    }

    ////lelijke gele circel in editor
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position + movement.normalized, 0.5f);
    //}
}
