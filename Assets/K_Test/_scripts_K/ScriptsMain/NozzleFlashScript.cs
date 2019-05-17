using UnityEngine;
using System.Collections;

public class NozzleFlashScript : MonoBehaviour
{
    public ParticleSystem flashParticle;

    void Start()
    {
        flashParticle.Stop();
    }

    private void Update()
    {
        flashParticle.Play();
        Destroy(this.gameObject, 0.1f);
    }

}