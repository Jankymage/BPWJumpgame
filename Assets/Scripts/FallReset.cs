using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    //plak dit script op de trigger waarin de player kan vallen.
    //als de player in de trigger komt, zal deze verplaats worden naar de plek waar resetPoint zich bevind.
    
    public Transform player, resetPoint;
    Vector3 reset;

    private void Start()
    {
        reset = resetPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            player.position = reset;

        }

    }

}
