using System.Collections;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float activeTimer;

    private void Start()
    {
        gameManager = GameManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player) && !gameManager.ima)
        {
            gameManager.ima = true;
            StartCoroutine(ActivePowerUp());
            spriteRenderer.enabled = false;
        }
    }
    private IEnumerator ActivePowerUp()
    {
        print("VOu esperar");

        yield return new WaitForSeconds(activeTimer);

        print("ESperei");

        gameManager.ima = false;
    }
}
