using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LifeEventSO healthChangedEvent;

    private int health = 100;
    private Rigidbody rb;
    private Vector2 moveInput;
    private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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

    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}