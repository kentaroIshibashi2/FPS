using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

    public Text startText;
    AudioSource startAudio;
    public AudioClip startClip;

    private void Start()
    {
        startAudio = GetComponent<AudioSource>();
    }

    public void ButtonClick()
    {
        startText.color = new Color(0,1,1,1);

        startAudio.PlayOneShot(startClip);

        StartCoroutine("PlayGame");
    }


    IEnumerator PlayGame()
    {
      
            yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Player_Test");
    }
}
