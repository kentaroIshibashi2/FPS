using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoPlayerScript : MonoBehaviour {

    public AudioSource decoAudio;
    public AudioClip decoClip;
    bool collisionBool = true; //落下などで二度呼ばせないため
	
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collisionBool)
        {
            collisionBool = false;
            decoAudio.Play();
            Destroy(this.gameObject, 7.0f);
        }
    }
}
