using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Transition : MonoBehaviour
{
    [SerializeField] private float _delayBeforeFadeIn;
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _delayBetweenFades;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Image _img;

    private bool _hasFadeInFinished = false;

    public bool HasFadeInFinished => _hasFadeInFinished;
    private void OnEnable()
    {
        _hasFadeInFinished = false;
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        float i = 0;

        yield return new WaitForSeconds(_delayBeforeFadeIn);

        while (i < 1)
        {
            _img.color = new Color(0, 0, 0, i);
            i -= Time.deltaTime / _fadeDuration;

            yield return null;
        }

        _hasFadeInFinished = true;

        yield return new WaitForSeconds(_delayBetweenFades);

        while (i > 0)
        {
            _img.color = new Color(0, 0, 0, i);
            i += Time.deltaTime / _fadeDuration;

            yield return null;
        }

        yield return null;
        this.gameObject.SetActive(false);
    }
}
