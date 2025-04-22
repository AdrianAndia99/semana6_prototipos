using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIManager : MonoBehaviour
{
 
    [SerializeField] private LifeEventSO healthChangedEvent; 

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void OnEnable()
    {
        healthChangedEvent.OnEventRaised += UpdateHealthUI;
    }
    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        healthChangedEvent.OnEventRaised -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int newHealth)
    {
       

        if (newHealth < 30)
        {
            StartCoroutine(FadeOutUI());
        }
    }

    private IEnumerator FadeOutUI()
    {
        fadeImage.gameObject.SetActive(true);
        Color startColor = fadeImage.color;
        startColor.a = 0f;

        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            float alphaValue = Mathf.Lerp(startColor.a, 1f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeInUI()
    {
        Color startColor = fadeImage.color;
        startColor.a = 1f;

        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            float alphaValue = Mathf.Lerp(startColor.a, 0f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
