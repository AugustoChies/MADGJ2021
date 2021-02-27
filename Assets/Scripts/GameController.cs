using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState _gameState;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _transitionImage;
    [SerializeField] private PlayerLightningSpawn _playerDeathSpawn;
    [SerializeField] private Transform _playerStartPosition;


    private void Awake()
    {
        instance = this;
    }

    public IEnumerator InstantiatePlayerDeath(GameObject player, Vector2 collisor)
    {
        _transitionImage.SetActive(true);
        Transition transition = _transitionImage.GetComponent<Transition>();
        _gameState = GameState.Cutscene;
        _player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _player.GetComponent<BoxCollider2D>().enabled = false;

        while(!transition.HasFadeInFinished)
        {
            yield return null;
        }

        _playerDeathSpawn.InstantiatePlayerDeath(collisor);
        player.transform.position = _playerStartPosition.position;
        _player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        _player.GetComponent<BoxCollider2D>().enabled = true;
        
        while(_transitionImage.activeSelf)
        {
            yield return null;
        }
        _gameState = GameState.Gameplay;
    }
}
