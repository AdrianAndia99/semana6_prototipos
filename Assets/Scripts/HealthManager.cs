using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private LifeEventSO healthChangedEvent;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        healthChangedEvent.OnEventRaised += UpdateHealthBar;
    }

    private void OnDisable()
    {
        healthChangedEvent.OnEventRaised -= UpdateHealthBar;
    }
    private void Start()
    {
        UpdateHealthBar((int)healthSlider.value);
    }
    private void UpdateHealthBar(int newHealth)
    {
        healthSlider.value = newHealth;
        healthText.text = $"Vida: {newHealth} / {(int)healthSlider.maxValue}";
        Debug.Log("Nueva vida: " + newHealth);
    }
}
