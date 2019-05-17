using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TokuPlayerController : MonoBehaviour {


	public GameObject ak47;　//ハンドガン
	public AudioClip shotSound; //発砲音
	public AudioClip reloadSound;
    public AudioClip zeroSound;
    public AudioClip gunSet;
	public float range = 100;　//rayの射程距離
	Ray ray;
	RaycastHit hit;　
	public GameObject nozzle;//銃口
    public GameObject nozzleEffect;
	Vector3 cameraCenter ; //銃の標準用
	public int bulletBox = 100;
	public int bullet = 30;

    public int m4bulletBox = 40;
    public int m4bullet = 20;
	int shotcount ;　//リロードするかの変数
	AudioSource audioSource;
    public Camera camera;
    bool zoom;　//ズームしてるか
    public static bool shot;　//ハンドガンを撃てるか
    bool m4shot;//M4を撃てるか
    bool sit;　//しゃがんでいるか
    bool ak;　//ハンドガンを構えているか
    bool m4;　//M4を構えているか
    bool m4Get;　//M4を入手しているか
    public static bool restock;　//銃弾回復をしているか
    public Image reticleImage;
    public Image zoomImage;
    public Text shellNum;
    public Text shellBox;
    public Text hgText;
    public Text m4Text;
    public GameObject M4;
    public GameObject effectobj;
    Animator anim;
    private int time;

    public GameObject AudioObject;
    AudioSource audioObjSource;

    public void YesShot()
    {
        shot = true;
        restock = true;
        m4shot = true;
    }

    public void NoShot()
    {
        shot = false;
        restock = false;
        m4shot = false;
    }
    

  

	
	void Start () {
		
		audioSource = GetComponent<AudioSource> ();
        zoomImage.enabled = false;
        zoom = false;
        shot = true;
        m4shot = false;
        sit = false;
        ak = true;
        m4 = false;
        restock = true;
        M4.SetActive(false);
        anim = GetComponent<Animator>();

        hgText.enabled = true;
        m4Text.enabled = false;

        //AudioObject = GameObject.Find("Audio");
        audioObjSource = AudioObject.GetComponent<AudioSource>();
        
    }
	
	

	void Update () {

        time++;

        if (ak && !m4)//hand gun を構えているとき
        {
            shellNum.text = bullet + "  ";
            shellBox.text = "/ " + bulletBox + "";

            //銃の発砲機能(hand gun)
            if (bullet > 0 && shot == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shot();
                }
            }
            else if (bullet <= 0 && !shot)
            {
                if (Input.GetMouseButtonDown(0))
                    audioObjSource.PlayOneShot(zeroSound);
            }
            //リロード機能
            if (Input.GetKeyDown("r") && restock)
            {
                if (bullet < 30 && bulletBox > 0)
                {
                    shot = false;
                    StartCoroutine("Reload");
                }
            }
        }
        else if (!ak && m4)//rifle　を構えているとき
        {
            shellNum.text = m4bullet + "  ";
            shellBox.text = "/ " + m4bulletBox + "";

            //銃の発砲機能(rifle)
            if (m4bullet > 0 && m4shot == true)
            {
                if (Input.GetMouseButton(0))
                {
                    if (time % 5 == 0) //M4の連射
                    {
                        RifleShot();
                    }
                }
            }
            else if(m4bullet<=0 && !m4shot)
            {
                if(Input.GetMouseButtonDown(0))
                audioObjSource.PlayOneShot(zeroSound);

            }
          
            //リロード機能
            if (Input.GetKeyDown("r") && restock)
            {
                if (m4bullet < 20 && bulletBox > 0)
                {
                    m4shot = false;
                    StartCoroutine("Reload");
                }
            }
        }


        if (bullet <= 0 && ak){
            shellNum.color = Color.red;
        }
        else if(bullet>0 && ak)
        {
            shellNum.color = Color.white;
        }

        if (bulletBox <= 0 && ak)
        {
            shellBox.color = Color.red;
        }
        else if(bulletBox>0 && ak)
        {
            shellBox.color = Color.white;
        }


        if (m4bullet <= 0 && m4)
        {
            shellNum.color = Color.red;
        }
        else if (m4bullet > 0 && m4)
        {
            shellNum.color = Color.white;
        }

        if (m4bulletBox <= 0 && m4)
        {
            shellBox.color = Color.red;
        }
        else if (m4bulletBox > 0 && m4)
        {
            shellBox.color = Color.white;
        }

        //しゃがみ機能
        if (Input.GetKeyDown(KeyCode.C))
        {
            SitActive();
        }

        if(bullet <= 0) {

            shot = false;

            if (Input.GetMouseButtonDown(0))
            {
                audioObjSource.PlayOneShot(zeroSound);
            }
        }

        if(m4bullet <= 0)
        {
            m4shot = false;
        }

        //ズームしてるか
        if (Input.GetMouseButtonDown(1)) {
            if(zoom == false)
            {
                zoom = true;
                ZoomStart();
            }
            else
            {
                zoom = false;
                ZoomStop();
            }
        }


        if(m4Get&& shot) //M4をGETして武器の切り替えができるか
        {
            if (Input.GetKeyDown("1"))
            {
                SetAk();
            }else if (Input.GetKeyDown("2"))
            {
                SetM4();
            }
        }

        if (Input.GetKey("left shift") && !sit)
        {
            audioSource.volume = 0.8f;
        }
        else if (!Input.GetKey("left shift") && !sit)
        {
            audioSource.volume = 0.6f;
        }
        else if (Input.GetKey("left shift") && sit)
        {
            audioSource.volume = 0.4f;
        }
        else if (!Input.GetKey("left shift") && sit)
        {
            audioSource.volume = 0.05f;
        }

        }





    public void M4active()
    {
        m4Get = true;
    }






    void Shot()
    {
        cameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (sit)
        {
            cameraCenter.y = cameraCenter.y - 28;
           
        }
        ray = Camera.main.ScreenPointToRay(cameraCenter);
        audioObjSource.PlayOneShot(shotSound); //AudioObjで鳴らす
        bullet--;
        shotcount++;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider)
            {

                Instantiate(effectobj, hit.point, Quaternion.identity);
                Instantiate(nozzleEffect, nozzle.transform.position, Quaternion.identity);
            }
        }
    }


        void RifleShot()
        {
        cameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (sit)
        {
            cameraCenter.y = cameraCenter.y - 28;

        }
        ray = Camera.main.ScreenPointToRay(cameraCenter);
        audioObjSource.PlayOneShot(shotSound);
        m4bullet--;
        shotcount++;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider)
            {

                Instantiate(effectobj, hit.point, Quaternion.identity);
                // Instantiate(effectobj, nozzle.transform.position, Quaternion.identity);
            }
        }
    } 

		

        IEnumerator Reload(){
           audioObjSource.PlayOneShot(reloadSound);

        if (ak && !m4){

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

        }else if(!ak && m4){

            m4bullet = m4bullet + m4bulletBox;
            if (m4bullet >= 20)
            {
                m4bullet = 20;
            }
            m4bulletBox = m4bulletBox - shotcount;
            if (m4bulletBox <= 0)
            {
                m4bulletBox = 0;
            }
            shotcount = 0;

        }

        yield return new WaitForSeconds(2.5f);

        shot = true;
        m4shot = true;

         }
   

        
    void ZoomStart()
    {
        camera.fieldOfView = 20;
        reticleImage.enabled = false;
        zoomImage.enabled = true;
      //  anim.SetBool("IsScope", true);
    }

    void ZoomStop()
    {
        camera.fieldOfView = 60;
        zoomImage.enabled = false;
        reticleImage.enabled = true;
      //  anim.SetBool("IsScope", false);
    }

    void SitActive() //しゃがみ判定による
    {
        if (sit == false)
        {

            sit = true;

            anim.SetBool("IsSit2", true);
        }
        else
        {
            sit = false;

            anim.SetBool("IsSit2", false);
        }
    }

    public void StopReload()
    {
        StopCoroutine("Reload");
    }


    public void Restock()
    {
        shot = false;

        restock = false;

        StopCoroutine("Reload");
            
        StartCoroutine("RestockCorotine");
    }

    public void StopRestock()
    {
        shot = true;

        restock = true;

        StopCoroutine("RestockCorotine");
    }



    IEnumerator RestockCorotine()
    {
        while (bulletBox < 100)
        {
            yield return new WaitForSeconds(1.0f);

            if (bulletBox < 100)
            {
                bulletBox++;
            }
        }
    }


    void SetAk()//HandGunを構えた時
    {

        m4 = false;
        ak = true;
        m4shot = false;

        audioObjSource.PlayOneShot(gunSet);
        M4.SetActive(false);
        ak47.SetActive(true);

        m4Text.enabled = false;
        shellNum.text = bullet + "  ";
        shellBox.text = "/ " + bulletBox + "";
        hgText.enabled = true;
    }

    void SetM4()//M4を構えた時
    {

        ak = false;
        m4 = true;
        m4shot = true;

        audioObjSource.PlayOneShot(gunSet);
        ak47.SetActive(false);
        M4.SetActive(true);

        hgText.enabled = false;

        shellNum.text = "aa" + "  ";
        shellBox.text = "/ " + "bb" + "";
        m4Text.enabled = true;
    }
}
	