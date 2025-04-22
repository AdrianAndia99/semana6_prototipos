using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LifeEventSO healthChangedEvent;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnMinRange;
    [SerializeField] private Vector3 spawnMaxRange;

    private int health = 100;
    private Rigidbody rb;
    private Vector2 moveInput;
    private float moveSpeed = 5f;
    private Renderer playerRenderer;
    private Color originalColor;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.color;
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
            SpawnObject();
            StartCoroutine(ChangeColorTemporarily(Color.red, 0.5f));
        }
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void SpawnObject()
    {
        float x = Random.Range(spawnMinRange.x, spawnMaxRange.x);
        float y = Random.Range(spawnMinRange.y, spawnMaxRange.y);
        float z = Random.Range(spawnMinRange.z, spawnMaxRange.z);
        Vector3 spawnPosition = new Vector3(x, y, z);

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
    private IEnumerator ChangeColorTemporarily(Color newColor, float duration)
    {
        playerRenderer.material.color = newColor;
        yield return new WaitForSeconds(duration);
        playerRenderer.material.color = originalColor;
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}