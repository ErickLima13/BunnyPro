using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PainfulSmile.Runtime.Core.Singleton<GameManager>
{

    public int idCenario;
    public Material[] BG0, BG1, BG2, BG3;

    [Header("Geracao mapa fase")]
    public float tamanhoCenario;
    public int qtdCenarios;
    public GameObject[] hillPrefabs;

    private void Start()
    {
        Criarfase();
    }

    private void Criarfase()
    {
        GameObject temp = null;
        int rand;

        for (int i = 1;  i <= qtdCenarios; i++)
        {
            rand = Random.Range(0,hillPrefabs.Length);
            temp = Instantiate(hillPrefabs[rand],transform.position + new Vector3(i * tamanhoCenario,0,0),transform.localRotation);
        }

        temp = Instantiate(hillPrefabs[0], transform.position + new Vector3(qtdCenarios * tamanhoCenario, 0, 0), transform.localRotation);
    }
    

}
