using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string PlayerDeathName = "PlayerDeath";
    private const string PlayerIdleName = "PlayerIdle";

    public static GameController instance;
    public GameState _gameState;

    [SerializeField] private GameObject _player,_playerCutscene;
    [SerializeField] private GameObject _transitionImage;
    [SerializeField] private string _menuScene;
    [SerializeField] private PlayerLightningSpawn _playerDeathSpawn;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private float _levelTransitionWaitTime,_lastSceneTransitionTime;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.SetString(Menu.SceneLoadPref, SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
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
    public IEnumerator GoToNextScene(string next)
    {
        _gameState = GameState.Cutscene;
        PlayerAnimation.CurrentPlayerState = PlayerState.Idle;
        _player.GetComponent<Animator>().Play(PlayerIdleName);

        yield return new WaitForSeconds(_levelTransitionWaitTime);
        _transitionImage.SetActive(true);

        Transition transition = _transitionImage.GetComponent<Transition>();

        while (!transition.HasFadeInFinished)
        {
            yield return null;
        }

        SceneManager.LoadScene(next, LoadSceneMode.Single);
    }

    public IEnumerator EndScene(Vector3 cutscenePos)
    {
        _gameState = GameState.Cutscene;
        PlayerAnimation.CurrentPlayerState = PlayerState.Idle;
        _player.GetComponent<Animator>().Play(PlayerIdleName);

        Instantiate(_playerCutscene, cutscenePos, Quaternion.identity);
        yield return new WaitForSeconds(_lastSceneTransitionTime);

        yield return new WaitForSeconds(_levelTransitionWaitTime);
        _transitionImage.SetActive(true);

        Transition transition = _transitionImage.GetComponent<Transition>();

        while (!transition.HasFadeInFinished)
        {
            yield return null;
        }

        SceneManager.LoadScene(_menuScene, LoadSceneMode.Single);
    }


}
