using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour {

    AudioSource worpAudio;

    public AudioClip worpClip;

    private void Start()
    {
        worpAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            worpAudio.PlayOneShot(worpClip);

            other.gameObject.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+12,transform.transform.position.z);
        }
    }
}
