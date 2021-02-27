using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState _gameState;
  
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

        _playerDeathSpawn.InstantiatePlayerDeath(collisor);
        player.transform.position = _playerStartPosition.position;
        yield return null;
    }
}
