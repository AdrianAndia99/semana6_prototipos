using UnityEngine;
using System.Collections;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private LifeEventSO m_LifeEventSO;
    private Vector3 originalScale;

    private void OnEnable()
    {
        m_LifeEventSO.OnEventRaised += ReactToPlayerHit;
    }

    private void OnDisable()
    {
        m_LifeEventSO.OnEventRaised -= ReactToPlayerHit;
    }

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void ReactToPlayerHit(int currentHealth)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleReaction());
    }

    private IEnumerator ScaleReaction()
    {
        Vector3 targetScale = originalScale * 1.3f;
        float duration = 1f;
        float t = 0;

        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        yield return new WaitForSeconds(0.1f);

        t = 0;
        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
