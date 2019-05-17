using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject effectObj;
    AudioSource audioSource;
    public AudioClip objClip;
    public ParticleSystem objParticle;
    public ParticleSystem playerParticle;
    GameObject player;
    HpSliderScript script; 
    int damagehp = 50;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        script = player.GetComponent<HpSliderScript>();
        damagehp = script.hp;

        objParticle.Stop();
        playerParticle.Stop();
	}



    void OnCollisionEnter(Collision other){

        if (other.gameObject.tag != "Player")
        {

            objParticle.Play();

        }
        else
        {
            playerParticle.Play();
        }

        Destroy(this.gameObject, 0.1f);
    }
}