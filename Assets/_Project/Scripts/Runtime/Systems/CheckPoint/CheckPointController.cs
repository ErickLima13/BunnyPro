using System.Collections;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.player.OnPlayerTakeHit += ResetPosistion;
    }

    private void ResetPosistion()
    {
        StartCoroutine(HitDelay());
    }

    private IEnumerator HitDelay()
    {
        PlayerController player = gameManager.player;   

        player.horizontal = 0;

        yield return new WaitForSeconds(0.3f);

        if (gameManager.currentCheckPoint != null)
        {
            player.transform.position = gameManager.currentCheckPoint.position;         
        }
        else
        {
            player.transform.position = gameManager.startPos;
        }

        yield return new WaitUntil(() => player.onVisible.Visible);

        StartCoroutine(gameManager.StartGameplay());

        player.isHit = false;
    }

    private void OnDestroy()
    {
        gameManager.player.OnPlayerTakeHit -= ResetPosistion;
    }

}
