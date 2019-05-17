using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour {

    public Text EnemyNum;
    
    GameObject [] tagObject;

	// Use this for initialization
	void Start () {
       // tagObject = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        tagObject = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyNum.text = "Enemies × " + tagObject.Length;

        if (tagObject.Length == 0) SceneManager.LoadScene("WinnerScene");
	}
}
