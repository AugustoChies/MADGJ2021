using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class InitialFade : MonoBehaviour
{
    [SerializeField] private float _delayBeforeFadeOut;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private Image _img;
    private void Start()
    {
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        GameController.instance._gameState = GameState.Cutscene;

        float i = 1;

        yield return new WaitForSeconds(_delayBeforeFadeOut);

        while (i > 0)
        {
            _img.color = new Color(0, 0, 0, i);
            i -= Time.deltaTime;
            yield return null;
        }

        GameController.instance._gameState = GameState.Gameplay;

        this.gameObject.SetActive(false);
    }
}
