using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_PlayerContoroller : MonoBehaviour
{


    public GameObject ak47;
    AudioClip shotSound; //発砲音
    AudioClip reloadSound;
    public float range = 100; //rayの射程距離
    Ray ray;
    RaycastHit hit;
    public GameObject nozzle; //銃口
    Vector3 cameraCenter; //銃の標準用
    public int bulletBox = 150;
    public int bullet = 30;
    int shotcount;
    int PlayerLife = 5;
    AudioSource audioSource;
   

    // Use this for initialization
    void Start()
    {
        shotSound = Resources.Load<AudioClip>("Audio/fire"); //shot時の音
        reloadSound = Resources.Load<AudioClip>("Audio/reload");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //しゃがみ機能
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            ak47.transform.localScale = new Vector3(1, 1 / 0.5f, 1);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 1, 1);
            ak47.transform.localScale = new Vector3(1, 1, 1);
        }

        //銃の発砲機能
        if (bullet > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine("Shot");
            }
        }
        //リロード機能
        if (Input.GetKeyDown("r"))
        {
            if (bullet < 30 && bulletBox > 0)
            {
                reload();
            }
        }

    }


    
    void reload()
    {
        audioSource.PlayOneShot(reloadSound);
        bullet = bullet + bulletBox;
        if (bullet >= 30)
        {
            bullet = 30;
        }
        bulletBox = bulletBox - shotcount;
        if (bulletBox <= 0)
        {
            bulletBox = 0;
        }
        shotcount = 0;


    }
    
}
