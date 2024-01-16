using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PainfulSmile.Runtime.Core.Singleton<GameManager>
{

    public enum ModoJogo
    {
        Desenvolvimento,
        Producao
    }

    public ModoJogo modo;

    [Header("Config Cenario Bg")]
    public int idCenario;
    public Transform backGrounds;
    public Material[] BG0, BG1, BG2, BG3;
    public Color[] skyColor;

    [Header("Geracao mapa fase")]
    public float tamanhoCenario;
    public int qtdCenarios;
    public GameObject[] hillPrefabs;

    [Header("Camera config")]
    public float speedCam;
    public float offsetX;
    public float offsetY;
    public Transform downLimit;
    public Transform upLimit;
    public Transform leftLimit;
    public Transform rightLimit;
    public Camera cam;

    public PlayerController player;
    private Vector3 posPlayer;

    private AudioController audioController;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (modo == ModoJogo.Producao)
        {
            audioController = FindObjectOfType<AudioController>();
            audioController.FadeOut();
        }
      

        Criarfase();
    }

    private void FixedUpdate()
    {
        MoveCam();
    }

    private void Criarfase()
    {
        GameObject temp = null;
        int rand;

        for (int i = 1; i <= qtdCenarios; i++)
        {
            rand = Random.Range(0, hillPrefabs.Length);
            temp = Instantiate(hillPrefabs[rand], transform.position + new Vector3(i * tamanhoCenario, 0, 0), transform.localRotation);
        }

        temp = Instantiate(hillPrefabs[0], transform.position + new Vector3(qtdCenarios * tamanhoCenario, 0, 0), transform.localRotation);
        rightLimit.position = temp.transform.position;

        if ( audioController!= null && modo == ModoJogo.Producao )
        {
            audioController.FadeOut();
        }

    }

    private void MoveCam()
    {
        posPlayer = new Vector3(player.transform.position.x + offsetX,
            player.transform.position.y + offsetY, -10);


        if (posPlayer.x <= leftLimit.position.x)
        {
            posPlayer = new(leftLimit.position.x, posPlayer.y, -10);
        }
        else if (posPlayer.x >= rightLimit.position.x)
        {
            posPlayer = new(rightLimit.position.x, posPlayer.y, -10);
        }

        if (posPlayer.y <= downLimit.position.y)
        {
            posPlayer = new(posPlayer.x, downLimit.position.y, -10);
        }
        else if (posPlayer.y >= upLimit.position.y)
        {
            posPlayer = new(posPlayer.x, upLimit.position.y, -10);
        }

        cam.transform.position =
            Vector3.MoveTowards(cam.transform.position, posPlayer, speedCam);

        backGrounds.position = new(cam.transform.position.x, 0, 0);
    }


}
