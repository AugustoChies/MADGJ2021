using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StageEndLight : MonoBehaviour
{
    public Light2D myLight;
    public float maxExpansion, maxintensity, transitiontime;
    protected float initradius,initIntensity;

    private void Start()
    {
        initIntensity = myLight.intensity;
        initradius = myLight.pointLightOuterRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shine());
        }
    }

    IEnumerator Shine()
    {
        for (float i = 0; i < 1; i+= Time.deltaTime/transitiontime)
        {
            yield return null;
            myLight.pointLightOuterRadius = Mathf.Lerp(initradius,maxExpansion,i);
            myLight.intensity = Mathf.Lerp(initIntensity, maxintensity, i);
        }
        //Activate FadeOut
    }
}
