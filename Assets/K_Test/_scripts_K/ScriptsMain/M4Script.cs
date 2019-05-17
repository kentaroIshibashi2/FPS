using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4Script : MonoBehaviour
{

    GameObject player;
    TokuPlayerController script;
    public AudioClip getItem;
    private AudioSource audiosource;
    string playername = "";

    void Start()
    {
        player = GameObject.Find("Player"); 
        script = player.GetComponent<TokuPlayerController>();

        audiosource = gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !playername.Contains((other.gameObject.name))) 
        {
            playername = playername + other.gameObject.name; //二回目以降は呼ばれないよう
            audiosource.PlayOneShot(getItem);
            script.M4active();
        }
    }
}