using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    public GameObject shadow,spawnPos;
    public SpriteRenderer sprite;
    public float cooldown;
    protected float currentT;


    private void Awake()
    {
        sprite.enabled = false;
        currentT = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(currentT > 0)
        {
            currentT -= Time.deltaTime;           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& currentT <= 0)
        {
            Instantiate(shadow, spawnPos.transform.position, Quaternion.identity);
            currentT = cooldown;
        }
    }
}
