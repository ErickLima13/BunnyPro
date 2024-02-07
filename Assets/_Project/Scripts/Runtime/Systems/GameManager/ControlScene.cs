using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    private AudioController audioController;
    public Fase fase;

    public Sprite[] stars;

    [Header("HUD")]
    public int qtdCenouras;
    public int qtdDiamantes;
    public int qtdEstrelas;
    public TextMeshProUGUI cenourasText;
    public TextMeshProUGUI diamantesText;

    public GameObject avatarPin;

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

        qtdCenouras = PlayerPrefs.GetInt("qtdCenouras");
        qtdDiamantes = PlayerPrefs.GetInt("qtdDiamantes");
        //qtdEstrelas = PlayerPrefs.GetInt("qtdEstrelas");

        cenourasText.text = qtdCenouras.ToString();
        diamantesText.text = qtdDiamantes.ToString();
       
    }

    public void BtnFase()
    {
        audioController.TrocarCena("0", true, audioController.fase);
    }
}
