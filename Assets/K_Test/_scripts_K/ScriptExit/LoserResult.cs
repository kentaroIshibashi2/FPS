using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoserResult : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip loserClip;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	public void ResultRoser()
    {
        audioSource.PlayOneShot(loserClip);

        SceneManager.LoadScene("StartScene");
    }
}
