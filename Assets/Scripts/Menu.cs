using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    protected Vector3 originalPos, originalScale;
    public Vector3 finalPos, finalScale;
    public float expansionTime;
    public RectTransform mainImage;
    public GameObject title, startbutton, creditsbutton, quitbutton, credits;
    protected bool begining,creditsMode;
    // Start is called before the first frame update
    void Start()
    {
        begining = true;
        originalPos = mainImage.anchoredPosition;
        originalScale = mainImage.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (begining)
            {
                begining = false;
                StartCoroutine(Zoom());
            }
            if(creditsMode)
            {
                ShowMenu();
            }
        }

    }

    protected void ShowMenu()
    {
        startbutton.SetActive(true);
        creditsbutton.SetActive(true);
        quitbutton.SetActive(true);
        credits.SetActive(false);
        creditsMode = false;
    }

    protected void ShowCredits()
    {
        startbutton.SetActive(false);
        creditsbutton.SetActive(false);
        quitbutton.SetActive(false);
        credits.SetActive(true);
        creditsMode = true;
    }

    IEnumerator Zoom()
    {
        title.SetActive(false);
        for (float i = 0; i <= 1; i+= Time.deltaTime/expansionTime)
        {
            mainImage.localPosition = Vector3.Lerp(originalPos, finalPos, i);
            mainImage.localScale = Vector3.Lerp(originalScale, finalScale, i);
            yield return null;
        }
        ShowMenu();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void CreditsButton()
    {
        ShowCredits();
    }

    public void QuittButton()
    {
        Application.Quit();
    }
}
