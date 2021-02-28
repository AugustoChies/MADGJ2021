using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] private float riseSpeed, cutsceneTime,risingTime, endPartEmission,endPartSpeed;
    private float initPos, initParticleEmission, initParticleSpeed;
    [SerializeField] private ParticleSystem particles;
    private bool rising;
    [SerializeField] private GameObject kabum;
    // Start is called before the first frame update
    void Start()
    {
        rising = true;
        initParticleEmission = particles.emission.rateOverTime.constant;
        initParticleSpeed = particles.main.startSpeed.constant;
        initPos = transform.position.y;
        StartCoroutine(ParticleIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        if (rising)
        {
            transform.Translate(0, riseSpeed* Time.deltaTime, 0);
            if(risingTime > 0)
            {
                risingTime -= Time.deltaTime;
                if(risingTime <= 0)
                {
                    rising = false;
                }
            }
        }
    }

    IEnumerator ParticleIncrease()
    {
        var emission = particles.GetComponent<ParticleSystem>().emission;
        var speed = particles.GetComponent<ParticleSystem>().main;
        for (float i = 0; i < 1; i += Time.deltaTime/cutsceneTime)
        {
            yield return null;
            emission.rateOverTime = Mathf.Lerp(initParticleEmission, endPartEmission, i);
            speed.startSpeed = Mathf.Lerp(initParticleSpeed, endPartSpeed, i);
        }

        Instantiate(kabum, this.transform.position, Quaternion.identity);
        this.GetComponent<SpriteRenderer>().enabled = false;
        particles.Stop();
    }
}
