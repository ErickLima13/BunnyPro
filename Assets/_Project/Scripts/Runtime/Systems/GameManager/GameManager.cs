using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum GameState
{
    Intro,
    Map,
    Gameplay,
    Hit
}

public class GameManager : PainfulSmile.Runtime.Core.Singleton<GameManager>
{
    public enum ModoJogo
    {
        Desenvolvimento,
        Producao
    }

    public ModoJogo modo;
    public bool isEndless;
    public GameState currentState;

    [Header("Config Cenario Bg")]
    public int idCenario;
    public Transform backGrounds;
    public Material[] BG0, BG1, BG2, BG3;
    public Color[] skyColor;

    [Header("Geracao mapa fase")]
    public bool isTransition;
    public float tamanhoCenario;
    public int qtdCenarios;
    public int idTema;
    public int newIdTema;

    //letras 
    public GameObject[] letters;
    public float[] percLetter;
    public List<GameObject> letra;
    public List<GameObject> cenarios;

    public GameObject transitionPrefab;
    public GameObject[] hillPrefabs;
    public GameObject[] hillPrefabsCheckPoints;

    public GameObject[] desertPrefabs;
    public GameObject[] desertPrefabsCheckPoints;

    [Header("Camera config")]
    public float speedCam;
    public float offsetX;
    public float offsetY;
    public Transform downLimit;
    public Transform upLimit;
    public Transform leftLimit;
    public Transform rightLimit;
    public Camera cam;

    [Header("HUD")]
    public Image checkA;
    public Image checkB;
    public Image checkC;
    public Image progressBar;
    public Image[] collectablesLetters;


    [Header("Variaveis do percentual do progresso")]
    public float distancia;
    public float distanciaAtual;
    public float perc;
    public Transform chegada;
    public float ajuste = 1.2f;

    public PlayerController player;
    private Vector3 posPlayer;

    private AudioController audioController;

    public Fase aFase;

    [Header("Start Game")]
    public Image gameplayImage;
    public Sprite[] spritesGameplay;

    private bool isNewMusic;

    private Transform[] checkPointsPos;

    public Transform currentCheckPoint;
    public Vector3 startPos;

    [Header("Power Ups")]
    public bool ima;
    public float distanceCollect;

    private void Start()
    {
        checkPointsPos = new Transform[2];

        player = FindObjectOfType<PlayerController>();

        startPos = player.transform.position;

        if (modo == ModoJogo.Producao)
        {
            audioController = FindObjectOfType<AudioController>();
            aFase = audioController.mFase;

            idCenario = aFase.idTema;
            isTransition = aFase.isTransition;
            qtdCenarios = aFase.qtdCenarios;
            newIdTema = aFase.newIdTema;

        }
        else
        {
            idCenario = 0;
        }

        Criarfase();

        gameplayImage.enabled = false;


    }

    private void Update()
    {
        if (!IsGameplay())
        {
            return;
        }

        CheckProgressBar();
    }

    private void FixedUpdate()
    {
        if (!IsGameplay())
        {
            return;
        }

        MoveCam();
    }

    private void CheckProgressBar()
    {
        distanciaAtual = Vector2.Distance(player.transform.position, chegada.position) - ajuste;
        perc = 1 - (distanciaAtual / distancia);

        if (perc < 0)
        {
            perc = 0;
        }
        else if (perc >= 0.99f)
        {
            perc = 1;
        }


        if (isTransition)
        {
            if (perc >= 0.66f && !isNewMusic)
            {
                idCenario = newIdTema;
                StartCoroutine(audioController.ChangeMusic(audioController.fase2));
                isNewMusic = true;
            }
        }

        if (perc >= 1f)
        {
            checkC.color = Color.white;
            LevelComplete();
        }
        if (perc >= 0.66f)
        {
            checkB.color = Color.white;
            currentCheckPoint = checkPointsPos[1];
        }
        else if (perc >= 0.33f)
        {
            checkA.color = Color.white;
            currentCheckPoint = checkPointsPos[0];
        }

        progressBar.fillAmount = perc;
    }

    private void Criarfase()
    {
        currentState = GameState.Gameplay;

        GameObject temp = null;
        GameObject pref = null;
        GameObject fimPrefab = null;
        int rand;
        int instanciados = 1;

        //parte 1 

        for (int i = 0; i < qtdCenarios; i++)
        {
            rand = Random.Range(0, hillPrefabs.Length);
            temp = Instantiate(hillPrefabs[rand], transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);
            instanciados++;
            cenarios.Add(temp);
        }

        temp = Instantiate(hillPrefabsCheckPoints[0], transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);
        checkPointsPos[0] = temp.transform;
        instanciados++;
        cenarios.Add(temp);

        //parte 2 

        for (int i = 0; i < qtdCenarios; i++)
        {
            rand = Random.Range(0, hillPrefabs.Length);
            temp = Instantiate(hillPrefabs[rand], transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);
            instanciados++;
            cenarios.Add(temp);
        }

        if (isTransition)
        {
            pref = transitionPrefab;
            idTema = newIdTema;
        }
        else
        {
            pref = hillPrefabsCheckPoints[1];
        }


        temp = Instantiate(pref, transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);
        checkPointsPos[1] = temp.transform;
        instanciados++;

        cenarios.Add(temp);

        //parte 3

        for (int i = 0; i < qtdCenarios; i++)
        {
            switch (idTema)
            {
                case 0:
                    rand = Random.Range(0, hillPrefabs.Length);
                    pref = hillPrefabs[rand];
                    fimPrefab = hillPrefabsCheckPoints[2];
                    break;
                case 1:
                    rand = Random.Range(0, desertPrefabs.Length);
                    pref = desertPrefabs[rand];
                    fimPrefab = desertPrefabsCheckPoints[2];
                    break;
            }

            temp = Instantiate(pref, transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);
            instanciados++;
            cenarios.Add(temp);
        }

        temp = Instantiate(fimPrefab, transform.position + new Vector3(instanciados * tamanhoCenario, 0, 0), transform.localRotation);

        chegada = GameObject.Find("FlagC").transform;

        rightLimit.position = temp.transform.position;

        distancia = Vector2.Distance(player.transform.position, chegada.position) - ajuste;


        // instanciando letras

        int idCenario = 1;

        foreach (GameObject g in cenarios)
        {
            int idLetra = letra.Count;

            if (letra.Count < 5)
            {
                int posLetra = Mathf.RoundToInt(cenarios.Count * percLetter[idLetra]);

                if (posLetra == idCenario)
                {
                    Transform t = g.GetComponent<SpawnLetter>().GetPoint();
                    GameObject letraTemp = Instantiate(letters[idLetra], t.position, t.localRotation);
                    letra.Add(letraTemp);
                }
            }
            idCenario++;
        }

        cenarios.Clear();

        if (audioController != null && modo == ModoJogo.Producao)
        {
            audioController.FadeOut();
        }

        StartCoroutine(StartGameplay());

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

    public IEnumerator StartGameplay()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            gameplayImage.enabled = true;
            yield return new WaitForSeconds(0.3f);
            gameplayImage.enabled = false;
            yield return new WaitForSeconds(0.3f);
        }

        player.horizontal = 1;
    }

    private void LevelComplete()
    {
        gameplayImage.sprite = spritesGameplay[2];
        gameplayImage.enabled = true;
        player.horizontal = 0;
        audioController.TrocarCena("Map", true, audioController.map);
        currentState = GameState.Map;

    }

    private bool IsGameplay()
    {
        return currentState == GameState.Gameplay;
    }
}
