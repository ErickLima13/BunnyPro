using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    private AudioController audioController;
    public Fase fase;

    public Sprite[] stars;

    [Header("Currencies")] // tranformar em scriptables object
    public int qtdCenouras;
    public int qtdDiamantes;
    public int qtdEstrelas;

    [Header("HUD")]
    public TextMeshProUGUI cenourasText;
    public TextMeshProUGUI diamantesText;
    public TextMeshProUGUI estrelasText;
    public GameObject hudCarrots;
    public GameObject hudStars;

    [Header("Loja")]
    public GameObject loja;


    public GameObject avatarPin;

    private void Initialization()
    {
        qtdCenouras = PlayerPrefs.GetInt("qtdCenouras");
        qtdDiamantes = PlayerPrefs.GetInt("qtdDiamantes");
        qtdEstrelas = PlayerPrefs.GetInt("qtdEstrelas");

        UpdateHud();

        loja.SetActive(false);
        hudCarrots.SetActive(!loja.activeSelf);
        hudStars.SetActive(loja.activeSelf);

        
    }

    void Awake()
    {
        audioController = FindObjectOfType<AudioController>();

        if (audioController == null)
        {
            SceneManager.LoadScene(0);
            return;
        }

        fase = audioController.mFase;
        audioController.FadeOut();
    }

    private void Start()
    {
        Initialization();
    }

    public void BtnFase()
    {
        audioController.TrocarCena("0", true, audioController.fase);
    }

    public void OpenStore()
    {
        loja.SetActive(!loja.activeSelf);
        hudCarrots.SetActive(!loja.activeSelf);
        hudStars.SetActive(loja.activeSelf);
    }

    public void UpdateHud()
    {
        cenourasText.text = qtdCenouras.ToString();
        diamantesText.text = qtdDiamantes.ToString();
        estrelasText.text = qtdEstrelas.ToString();
    }
}
