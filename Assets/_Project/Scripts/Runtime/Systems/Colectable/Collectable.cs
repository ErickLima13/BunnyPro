using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (gameManager.ima)
        {
            Transform target = gameManager.player.transform;
            float dist = Vector3.Distance(transform.position, target.position);

            if (dist <= gameManager.distanceCollect)
            {
                float velocidade = gameManager.player.speed + 2;
                transform.position = Vector3.MoveTowards(transform.position, target.position, velocidade * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            Destroy(gameObject);
        }
    }
}
