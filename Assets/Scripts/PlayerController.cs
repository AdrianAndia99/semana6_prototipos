using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LifeEventSO healthChangedEvent;

    private int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Max(0, health);
        healthChangedEvent.RaiseEvent(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}