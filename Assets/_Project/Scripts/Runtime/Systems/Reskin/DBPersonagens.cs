using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBPersonagens : MonoBehaviour
{
    public int idPersonagem;
    public int idPersonagemAtual;

    [Header("Dados personagem")] // Transformar em scriptables object
    public string[] nomePersonagem;
    public Texture[] spriteSheetName;
    public int[] pontosDeVida;
    public int[] velocidadeMovimento;
    public int[] forcaPulo;
    public bool[] puloDuplo;
    public bool[] liberado;
    public int[] precoEstrela;
    public int[] precoDiamantes;


    private void Start()
    {
        idPersonagemAtual = PlayerPrefs.GetInt("idPersonagemAtual");

        PlayerPrefs.SetInt("qtdCenouras", 40);
        PlayerPrefs.SetInt("qtdEstrelas", 40);
        PlayerPrefs.SetInt("qtdDiamantes", 40);
        
    }

}
