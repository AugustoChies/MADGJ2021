using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string PlayerDeathName = "PlayerDeath";
    private const string PlayerIdleName = "PlayerIdle";

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

    public IEnumerator InstantiatePlayerDeath(GameObject player, Vector2 collisor, bool isFlipped)
    {
        _gameState = GameState.Cutscene;

        _player.GetComponent<Animator>().Play(PlayerDeathName);
        _transitionImage.SetActive(true);
        Transition transition = _transitionImage.GetComponent<Transition>();
        //_player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        //_player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //_player.GetComponent<BoxCollider2D>().enabled = false;

        while(!transition.HasFadeInFinished)
        {
            yield return null;
        }

        _player.GetComponent<Animator>().Play(PlayerIdleName);

        _playerDeathSpawn.InstantiatePlayerDeath(collisor, isFlipped);

        player.transform.position = _playerStartPosition.position;
        //_player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        //_player.GetComponent<BoxCollider2D>().enabled = true;
        
        while(_transitionImage.activeSelf)
        {
            yield return null;
        }

        _gameState = GameState.Gameplay;
    }
}
