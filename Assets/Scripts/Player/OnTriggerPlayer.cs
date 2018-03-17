using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerPlayer : MonoBehaviour {

    //dit script zorgt er voor dat er bijgehouden word hoeveel objecten met de tag Block verzameld worden
    //als de in CollectLeft aangegeven hoeveelheid is verzameld, komt de text "you win" in de aangegeven text box.
    //De intensiteit van het licht dat de player uitstraalt gaat ook omhoog naarmate er meer crystals verzameld worden.
    //speelt ook geluid af als er een crystal verzameld word.

    public int CollectLeft = 10;
    public Text ColText;
    public Light playerLight;
    public AudioSource crystalSound;
    public RawImage credits;

    private Transform character;

    // Use this for initialization
    void Start () {
        ColText.text = "";
        character = GetComponent<Transform>();
        credits.gameObject.SetActive(false);
	}
	
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            other.gameObject.SetActive(false);
            CollectLeft -= 1;
            playerLight.intensity +=1;
            playerLight.range +=1;
            Instantiate(crystalSound, character);

            if (CollectLeft <= 0)
            {
                ColText.text = "You Win!";
                credits.gameObject.SetActive(true);
            }
        }
            
    }
}
