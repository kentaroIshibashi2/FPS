using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

	void Camera()
    {
        if (Input.GetKeyDown("c"))
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 0.13f, this.transform.localPosition.z + 0.03f);
        }
    }
		
	
}
