using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState _gameState;

    [SerializeField] private PlayerLightningSpawn _playerDeathSpawn;
    [SerializeField] private Transform _playerStartPosition;


    private void Awake()
    {
        instance = this;
    }

    public void InstantiatePlayerDeath(GameObject player, Vector2 collisor)
    {
        _playerDeathSpawn.InstantiatePlayerDeath(collisor);
        player.transform.position = _playerStartPosition.position;
    }
}
