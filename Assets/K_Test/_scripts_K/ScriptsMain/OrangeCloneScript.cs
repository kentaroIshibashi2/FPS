using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCloneScript : MonoBehaviour {　//撃ったら落ちてくるオレンジのスクリプト

    AudioSource audiosource;
    public AudioClip orangeClip;

    private void OnCollisionEnter(Collision collision)
    {
        audiosource = GetComponent<AudioSource>();

        if (collision.gameObject.tag == "Player")
        {
            audiosource.PlayOneShot(orangeClip);
            Destroy(this.gameObject, 0.1f);
        }
    }


}
