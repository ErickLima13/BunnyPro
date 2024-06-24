using UnityEngine;

public class SpawnLetter : MonoBehaviour
{
    public Transform[] spawnPoint;

    public Transform GetPoint()
    {
        Transform t = spawnPoint[Random.Range(0, spawnPoint.Length)];
        return t;
    }
}
