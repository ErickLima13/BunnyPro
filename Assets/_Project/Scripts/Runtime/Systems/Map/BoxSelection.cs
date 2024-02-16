using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxSelection : MonoBehaviour
{
    private DBPersonagens dB;
    private CharStore charStore;
    private ControlScene controlScene;

    public int idPersonagem;

    public Image personagem;
    public Image boxSelection;

    public TextMeshProUGUI nomePersonagem;
    public TextMeshProUGUI velocidade;
    public TextMeshProUGUI pontosVida;
    public TextMeshProUGUI forcaPulo;
    public TextMeshProUGUI precoEstrela;
    public TextMeshProUGUI precoDiamantes;

    public GameObject doubleJump;
    public GameObject painelLiberado;
    public GameObject painelLiberar;

    private void Start()
    {
        dB = FindObjectOfType<DBPersonagens>();
        charStore = FindObjectOfType<CharStore>();
        controlScene = FindObjectOfType<ControlScene>();

        UpdateBoxSelection();

        precoEstrela.text = dB.precoEstrela[idPersonagem].ToString();
        precoDiamantes.text = dB.precoDiamantes[idPersonagem].ToString();
        pontosVida.text = dB.pontosDeVida[idPersonagem].ToString();
        velocidade.text = dB.velocidadeMovimento[idPersonagem].ToString();
        forcaPulo.text = dB.forcaPulo[idPersonagem].ToString();
        doubleJump.SetActive(dB.puloDuplo[idPersonagem]);
    }

    public void LiberarPersonagem(int tipoMoeda)
    {
        switch (tipoMoeda)
        {
            case 0:
                if (controlScene.qtdEstrelas >= dB.precoEstrela[idPersonagem])
                {
                    dB.liberado[idPersonagem] = true;
                    controlScene.qtdEstrelas -= dB.precoEstrela[idPersonagem];
                    controlScene.UpdateHud();
                    UpdateBoxSelection();
                }
                break;
            case 1:
                if (controlScene.qtdDiamantes >= dB.precoDiamantes[idPersonagem])
                {
                    dB.liberado[idPersonagem] = true;
                    controlScene.qtdDiamantes -= dB.precoDiamantes[idPersonagem];
                    controlScene.UpdateHud();
                    UpdateBoxSelection();
                }
                break;
        }
    }

    public void SelecionarPersonagem()
    {
        if (dB.liberado[idPersonagem])
        {
            dB.idPersonagem = idPersonagem;
            UpdateBoxSelection();
        }
    }

    public void UpdateBoxSelection()
    {
        switch (dB.liberado[idPersonagem])
        {
            case true:
                painelLiberado.SetActive(true);
                painelLiberar.SetActive(false);
                personagem.color = Color.white;
                nomePersonagem.text = dB.nomePersonagem[idPersonagem].ToString();

                if (dB.idPersonagem == idPersonagem)
                {
                    boxSelection.sprite = charStore.bgBox[1];
                }
                else
                {
                    boxSelection.sprite = charStore.bgBox[0];
                }
                break;
            case false:
                boxSelection.sprite = charStore.bgBox[0];
                painelLiberado.SetActive(false);
                painelLiberar.SetActive(true);
                nomePersonagem.text = "?";
                personagem.color = Color.black;

                break;
        }
    }

    private void LateUpdate()
    {
        if (dB.liberado[idPersonagem])
        {
            if (dB.idPersonagem == idPersonagem)
            {
                boxSelection.sprite = charStore.bgBox[1];
            }
            else
            {
                boxSelection.sprite = charStore.bgBox[0];
            }
        }
    }
}
