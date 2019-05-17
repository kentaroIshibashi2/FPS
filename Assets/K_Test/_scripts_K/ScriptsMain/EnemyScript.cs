using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    public GameObject target;

    GameObject player;
    HpSliderScript script;

    NavMeshAgent agent;

    public Slider hpSlider;
    int fullhp;
    int hp = 100;

    GameObject[] decos;
    GameObject deco;

   

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        fullhp = hp;
        hpSlider.value = fullhp;

        player = GameObject.Find("Player");
        script = player.GetComponent<HpSliderScript>();
	}
	
	void Update () {

        decos = GameObject.FindGameObjectsWithTag("Deco");
        deco = GameObject.FindGameObjectWithTag("Deco");

        if (decos.Length > 0)
        {
            agent.destination = deco.transform.position;
        }
        else
        {
            agent.destination = target.transform.position;
        }

        hpSlider.value = hp;

        if (hp <= 0) Destroy(this.gameObject);
	}

     private void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.tag == "Bullet")
        {
            hp = hp - 1;
        }
     }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            script.Damage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            script.stopDamage();
        }
    }

}
