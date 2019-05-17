using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeScript : MonoBehaviour {　//木に生るオレンジ

    public GameObject orangeclone;
    Vector3 orangePosition = new Vector3(20, 10, 0);　

	void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject Mikan = GameObject.Find("OrangeClone(Clone)");　//何個も生まれないように

        if(other.gameObject.tag == "Bullet" && Mikan == null)
        {
            //Debug.Log("Orange!");

           
            Instantiate(orangeclone, orangePosition,orangeclone.transform.rotation);
        }
    }
}
