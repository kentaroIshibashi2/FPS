using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimScript : MonoBehaviour {

    Animator runanim;

	
	void Start () {
        runanim = GetComponent<Animator>();
	}
	
	void Update () {
        //走るアニメーション再生
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            runanim.SetBool("RunningBool", true);
        }

        //走るアニメーション停止
        if (!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
        {
            runanim.SetBool("RunningBool", false);
        }
    }

  
    
}
