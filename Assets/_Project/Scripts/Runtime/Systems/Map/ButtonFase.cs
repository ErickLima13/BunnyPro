using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFase : MonoBehaviour
{
    private ControlScene controlScene;

    public int qtdEstrelasFase; // salvar valor e resgatar no start

    [SerializeField] private TextMeshProUGUI faseText;
    [SerializeField] private GameObject block;

    public Transform avatarPinPos;

    public int idFase;
    public Sprite[] button;

    public Image btnImage;
    public Image estrelasFase;

    public GameObject requisito;
    public TextMeshProUGUI custoText;
    public int qtdCenouraCusto;

    public bool isBlock;

    private void Initialization()
    {
        controlScene = FindObjectOfType<ControlScene>();

        CheckBlock();


        estrelasFase.sprite = controlScene.stars[qtdEstrelasFase];
    }

    private void Start()
    {
        Initialization();
    }

    public void SetBlock(bool value)
    {
        isBlock = value;
        block.SetActive(isBlock);
    }

    public void SetTextNumber(string number)
    {
        faseText.text = number;
    }

    private void CheckBlock()
    {
        if (isBlock)
        {
            btnImage.sprite = button[0];
            block.SetActive(true);
            faseText.gameObject.SetActive(false);
            estrelasFase.gameObject.SetActive(false);
            custoText.text = qtdCenouraCusto.ToString();
            requisito.SetActive(true);
        }
        else
        {
            btnImage.sprite = button[1];
            block.SetActive(false);
            faseText.gameObject.SetActive(true);
            estrelasFase.gameObject.SetActive(true);
            requisito.SetActive(false);
        }
    }

    public void LiberarFase()
    {
        if (isBlock && controlScene.qtdCenouras >= qtdCenouraCusto)
        {
            controlScene.qtdCenouras -= qtdCenouraCusto;
            isBlock = false;
            CheckBlock();
        }
    }

    public void SelecionarFase()
    {
        if (!isBlock)
        {
            controlScene.avatarPin.transform.SetParent(transform);
            controlScene.avatarPin.transform.position = avatarPinPos.position;
        }
    }
}
