using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string PlayerDeathName = "PlayerDeath";
    private const string PlayerIdleName = "PlayerIdle";

    public static GameController instance;
    public GameState _gameState;

    [SerializeField] private string _nextLevelName;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _transitionImage;
    [SerializeField] private PlayerLightningSpawn _playerDeathSpawn;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private float _levelTransitionWaitTime;

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

        while(!transition.HasFadeInFinished)
        {
            yield return null;
        }

        _player.GetComponent<Animator>().Play(PlayerIdleName);
        PlayerAnimation.CurrentPlayerState = PlayerState.Idle;

        _playerDeathSpawn.InstantiatePlayerDeath(collisor, isFlipped);

        player.transform.position = _playerStartPosition.position;
        
        while(_transitionImage.activeSelf)
        {
            yield return null;
        }

        _gameState = GameState.Gameplay;
    }
    public IEnumerator GoToNextScene()
    {
        _gameState = GameState.Cutscene;
        yield return new WaitForSeconds(_levelTransitionWaitTime);
        _player.GetComponent<Animator>().Play(PlayerIdleName);
        PlayerAnimation.CurrentPlayerState = PlayerState.Idle;
        _transitionImage.SetActive(true);

        Transition transition = _transitionImage.GetComponent<Transition>();

        while (!transition.HasFadeInFinished)
        {
            yield return null;
        }

        SceneManager.LoadScene(_nextLevelName, LoadSceneMode.Single);
    }
}
