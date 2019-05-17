using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestockScript : MonoBehaviour {

    GameObject player;

    TokuPlayerController script;

    public ParticleSystem restockParticle;

	void Start () {
        player = GameObject.Find("Player"); 
        script = player.GetComponent<TokuPlayerController>();
        restockParticle.Stop();
    }

	void Update () {

	}


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            script.NoShot();
            restockParticle.Play();
            script.Restock();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            script.YesShot();
            restockParticle.Stop();
            script.StopRestock();
        }
    }
}
