using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpSliderScript : MonoBehaviour {


    public Slider hpSlider;
    public Image hpFill;
    int fullhp;
    public int hp = 50;

    AudioSource audiosource;
    public AudioClip damageClip;

    GameObject player;

    TokuPlayerController script;


    void Start () {
        fullhp = hp;

        player = GameObject.Find("Player");
        script = player.GetComponent<TokuPlayerController>();

        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        hpSlider.value = hp;

        if (hp <= fullhp/2 && hp > fullhp / 4)
        {
            hpFill.color = Color.yellow;
        }else if(hp <= fullhp / 4)
        {
            hpFill.color = Color.red;
        }
        else
        {
            hpFill.color = Color.white;
        }

        if (hp <= 0) SceneManager.LoadScene("GameOverScene"); 
    }


    private void OnTriggerEnter(Collider other) //敵と衝突した時
    {
        if (other.gameObject.tag == "Enemy")
        {
            hp = hp - 5;

            audiosource.PlayOneShot(damageClip);

            Debug.Log("!!!!!!!");
        }
    }

    public void Damage()
    {
        hp = hp - 5;

        audiosource.PlayOneShot(damageClip);

        StartCoroutine("DamageCorotine");
    }

    public void stopDamage()
    {
        StopCoroutine("DamageCorotine");
    }

        

    public void Recovery()
    {
        script.NoShot();//撃てない

        script.StopReload();

        StartCoroutine("RecoveryCorotine");
    }

    public void StopRecovery()
    {
        script.YesShot();//撃てる

        StopCoroutine("RecoveryCorotine");
    }



    IEnumerator RecoveryCorotine()//三秒ごとに回復
    {
        while (hp < 50)
        {
            yield return new WaitForSeconds(3.0f);

            if (hp < 50)   hp++ ;
        }
    }

    IEnumerator DamageCorotine()//三秒ごとに回復
    {
        while (hp > 0)
        {
            yield return new WaitForSeconds(1.0f);

            if (hp > 0) hp = hp - 5 ;
        }
    }
}
