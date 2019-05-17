using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecoScript: MonoBehaviour
{
    public Rigidbody OrangeBall;                  
    public Transform OrangeTransform;           
    public Slider DecoSlider;                  
    public Image DecoWhite;
    public Image DecoOne;
    public Image DecoTwo;
    
    
    public GameObject audioObj;         
    public AudioClip throwClip;            // 投げる音
    AudioSource audioSource;
    public float MinLaunchForce = 5f;        
    public float MaxLaunchForce = 20f;        
    public float MaxChargeTime = 1.5f;       


    private float CurrentLaunchForce;         
    private float ChargeSpeed;                
    private bool OrangeOK;                   
    private int orangenum;
    private int maxnum = 2;

    Animator animator;
    GameObject player;
    TokuPlayerController script;

    private void OnEnable()
    {
        CurrentLaunchForce = MinLaunchForce;
        DecoSlider.value = MinLaunchForce;
    }


    private void Start()
    {
        DecoWhite.enabled = true;
        DecoOne.enabled = false;
        DecoTwo.enabled = false;

        audioSource = audioObj.GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        script = player.GetComponent<TokuPlayerController>();
        animator = GetComponent<Animator>();

        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;

    }


    private void Update()
    {

        ImageChange();


        DecoSlider.value = MinLaunchForce;

        if (orangenum > 0)
        {
            // 最大力が増加していてまだ発射されていない場合...
            if (CurrentLaunchForce >= MaxLaunchForce && !OrangeOK)
            {
                // ... 最大力を使って砲弾を発射します
                CurrentLaunchForce = MaxLaunchForce;
                Fire();
            }
            // そうでない場合は、発射ボタンをちょうど押し始めた場合...
            else if (Input.GetKeyDown("e"))
            {
                script.NoShot();
                animator.SetBool("IsTrow2", false);
                animator.SetBool("IsTtrow1", true);

                // ...発射フラグと発射力をリセット
                OrangeOK = false;
                CurrentLaunchForce = MinLaunchForce;

            }
            // そうでない場合は、発射ボタンを押していて、かつ、まだ発射されていない場合...
            else if (Input.GetKey("e") && !OrangeOK)
            {
                // 発射力を増加してスライダーを更新
                CurrentLaunchForce += ChargeSpeed * Time.deltaTime;

                DecoSlider.value = CurrentLaunchForce;
            }
            // そうでない場合は、発射ボタンを離し、かつ、発射されていない場合...
            else if (Input.GetKeyUp("e") && !OrangeOK)
            {
                Fire();
            }
        }
    }


    private void Fire()
    {

        animator.SetBool("IsTtrow1", false);
        animator.SetBool("IsTrow2", true);

        orangenum--;

        // 発射フラグを設定して、Fire が1 度しか呼び出されないようにします。
        OrangeOK = true;

        Rigidbody orangeInstance =
            Instantiate(OrangeBall, OrangeTransform.position, OrangeTransform.rotation) as Rigidbody;

        orangeInstance.velocity = CurrentLaunchForce * OrangeTransform.forward; ;


        // もしもの時のために設定
        CurrentLaunchForce = MinLaunchForce;

        audioSource.PlayOneShot(throwClip);

        script.YesShot();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Orange")
        {
            if (orangenum < maxnum) orangenum++;
        }
    }


   void ImageChange()
    {
        if(orangenum == 0)
        {
            DecoWhite.enabled = true;
            DecoOne.enabled = false;
            DecoTwo.enabled = false;
        }else if(orangenum == 1)
        {
            DecoWhite.enabled = false;
            DecoOne.enabled = true;
            DecoTwo.enabled = false;
        }
        else
        {
            DecoWhite.enabled = false;
            DecoOne.enabled = true;
            DecoTwo.enabled = true;
        }
    }
}