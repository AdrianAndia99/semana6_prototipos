using UnityEngine;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private LifeEventSO healthChangedEvent;

    private void OnEnable()
    {
        healthChangedEvent.OnEventRaised += UpdateHealthBar;
    }

    private void OnDisable()
    {
        healthChangedEvent.OnEventRaised -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int newHealth)
    {
        Debug.Log("Nueva vida: " + newHealth);
    }
}
