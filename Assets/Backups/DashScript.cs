using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    //Script dat Valentijn heeft geschreven als voorbeeld voor de coroutine om te dashen
    private bool isDashing = false;
    private Rigidbody rb;
    public float dashDistance = 3;
    public float dashSpeed = 5;

    public Transform playerCam;

    //om de animatie van het springen wat vaart te geven
    //public AnimationCurve curve;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //roept de routine aan, nodig om het op deze manier te doen om te zorgen dat hij de yield gebruikt.
            StartCoroutine(MyRoutine(dashDistance, dashSpeed));
        }
    }

    //de coroutine
    IEnumerator MyRoutine(float dashDistance, float dashSpeed)
    {
        isDashing = true;
        rb.useGravity = false;

        float t = 0;

        Vector3 dir = playerCam.forward;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + dir.normalized * dashDistance;
        float dTime = dashDistance / dashSpeed;
        while (t < 1)
        {
            t += Time.deltaTime * 1 / dTime;
            //rb.MovePosition(Vector3.Lerp(startPos, endPos, curve.Evaluate(t)));
            transform.position = Vector3.Lerp(startPos, endPos, t);
            //yield om te zorgen dat de loop niet in 1 keer doorloopt, null = 1 frame
            yield return null;
        }
        rb.useGravity = true;
        isDashing = false;

    }
}
