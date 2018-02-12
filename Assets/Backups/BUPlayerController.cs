using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BUPlayerController : MonoBehaviour {

    //debugging text
    public Text Debug;


    public Transform PlayerCam, Character, CenterPoint;
    private CharacterController Player;
    public Text JumpText;
    public Text BlinkText;


    private float MouseX, MouseY;
    public float MouseSpeed = 10f;

    //voor movement
    private float MoveForward, MoveSide;
    public float MoveSpeed = 10F;
    private Vector3 Movement;
    public float RotationSpeed = 5f;

    //voor blink
    public float BlinkDist = 10f;
    private float BlinkMove;
    private int BlinkCount;
    public int MaxBlinkCount = 1;
    private int BlinkTimer;

    //voor springen
    private int JumpCount;
    public int MaxJumpCount = 2;
    public float JumpSpeed = 20f;
    public float GravMulti = 10F;
    private float VerticalVelocity;

    //voor zoomen camera
    public float Zoom;
    public float ZoomSpeed = 2;
    public float ZoomMin = -2f;
    public float ZoomMax = -10f;
    //voor hoogte camera
    public float CamYPosition = 1f;


    // Use this for initialization
    void Start()
    {

        JumpCount = MaxJumpCount;
        BlinkCount = MaxBlinkCount;

        Player = GameObject.Find("Player").GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.text = VerticalVelocity.ToString();

        //Voor het zoomen van de muis (met clamp)
        Zoom += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        if (Zoom > ZoomMin)
            Zoom = ZoomMin;
        if (Zoom < ZoomMax)
            Zoom = ZoomMax;
        PlayerCam.transform.localPosition = new Vector3(0, 0, Zoom);

        //draaien van de muis als rechtermuisknop ingedrukt word. (zorgt ook dat de muis verdwijnt)
        if (Input.GetMouseButton(1))
        {
            MouseX += Input.GetAxis("Mouse X") * MouseSpeed;
            MouseY -= Input.GetAxis("Mouse Y") * MouseSpeed;
            Cursor.visible = false;
        }
        else { Cursor.visible = true; }

        //Clampt de muis op boven en onderst as
        MouseY = Mathf.Clamp(MouseY, -60f, 60f);
        //zorgt dat de camera naar de centerpoint blijft kijken
        PlayerCam.LookAt(CenterPoint);
        //draait de camera afhankelijk van de muis stand
        CenterPoint.localRotation = Quaternion.Euler(MouseY, MouseX, 0);

        //Om te springen
        if (Player.isGrounded)
        {
            JumpCount = MaxJumpCount;
        }

        if (Input.GetButtonDown("Jump") && JumpCount >= 1)
        {
            VerticalVelocity = JumpSpeed;
            JumpCount -= 1;
        }

        JumpText.text = "Jumps = " + JumpCount.ToString();

        //Om te blinken
        if (Player.isGrounded)
        {
            BlinkCount = MaxBlinkCount;
        }
        if (Input.GetButtonDown("Fire3") && BlinkCount >= 1)
        {
            BlinkTimer = 10;
            BlinkCount -= 1;
        }

        if (BlinkTimer > 0)
        {
            BlinkTimer -= 1;
            BlinkMove = BlinkDist;
        }
        else
        {
            BlinkMove = 0;
        }

        BlinkText.text = "Blinks = " + BlinkCount.ToString();


        //neemt de input voor de beweging
        MoveForward = Input.GetAxis("Vertical") * MoveSpeed;
        MoveSide = Input.GetAxis("Horizontal") * MoveSpeed;
        //zet de input om naar de Vector3
        Movement = new Vector3(MoveSide, VerticalVelocity, MoveForward + BlinkMove);
        //verwerkt ook de rotatie van de character
        Movement = Character.rotation * Movement;
        
        //beweegt de character
        Player.Move(Movement * Time.deltaTime);
        CenterPoint.position = new Vector3(Character.position.x, Character.position.y + CamYPosition, Character.position.z);

        //zorgt dat de character roteerd
        if (Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0)
        {
            Quaternion TurnAngle = Quaternion.Euler(0, CenterPoint.eulerAngles.y, 0);
            Character.rotation = Quaternion.Slerp(Character.rotation, TurnAngle, Time.deltaTime * RotationSpeed);
        }

        

    }

    private void FixedUpdate()
    {

        float CurVel = VerticalVelocity;

        //check if player is not on the ground
        if (Player.isGrounded == false)
        {
            VerticalVelocity += Physics.gravity.y * Time.deltaTime;

            //als speler valt, krijgt hij extra snelheid.
            if (CurVel < -1.2 && CurVel > -2)
            {
                VerticalVelocity = VerticalVelocity * GravMulti;
            }
        }
        //check if the player is on the ground
        else
        {
            VerticalVelocity = 0f;
        }


        //moeilijk doen voor een blink met timer
        

        
    }

    

   
}


//private void OnTriggerEnter(Collider other)
//{
//    //to see if the character is on "ground", so it can reset the double jump.
//    if (other.CompareTag("Block"))
//    {
//        VerticalVelocity += Physics.gravity.y * Time.deltaTime;
//        Debug.text = "bump";
//    }
//}
