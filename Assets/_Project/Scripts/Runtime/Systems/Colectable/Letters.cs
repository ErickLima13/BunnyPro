using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letters : MonoBehaviour
{
    public int idLetter;

    private void Collect()
    {
        GameManager.Instance.collectablesLetters[idLetter].color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
