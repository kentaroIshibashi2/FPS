using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryScript : MonoBehaviour {

    GameObject player;
    HpSliderScript script;
    public ParticleSystem recoveryParticle;

    void Start()
    {
        player = GameObject.Find("Player");
        script = player.GetComponent<HpSliderScript>();
        recoveryParticle.Stop();
    }

    void Update()
    {

    }


     private void OnTriggerEnter(Collider other)
      {

          if (other.gameObject.tag == "Player")
          {
              recoveryParticle.Play();

              script.Recovery();　//回復させるコルーチン
          }
      }

      private void OnTriggerExit(Collider other)
      {

          if (other.gameObject.tag == "Player")
          {
              recoveryParticle.Stop();
              script.StopRecovery();
          }
      }


}
