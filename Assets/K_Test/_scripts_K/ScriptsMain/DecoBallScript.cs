using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoBallScript : MonoBehaviour {

    public GameObject decoPlayer;
    Vector3 clone = new Vector3(0, 0.8f, 0);

    private void OnCollisionEnter(Collision collision)
    {
      /*  if(collision.gameObject.tag == "Floor")
        {
            InstantClone();
        }*/
            Invoke("InstantClone", 5.0f); //垂直な壁の中などで発生させないため
    }

    void InstantClone()
    {
        Instantiate(decoPlayer, this.transform.position + clone, decoPlayer.transform.rotation);
        Destroy(this.gameObject);
    }
}
