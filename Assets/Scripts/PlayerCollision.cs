using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Killer")  && GameController.instance._gameState == GameState.Gameplay)
        {
            Vector2 collisionLocation = new Vector2(transform.position.x, collision.transform.position.y);
            bool isFlipped = transform.localScale.x < 0;

            StartCoroutine(GameController.instance.InstantiatePlayerDeath(this.gameObject, collisionLocation, isFlipped));
        }
    }
}
