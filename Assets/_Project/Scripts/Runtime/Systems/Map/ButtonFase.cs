using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFase : MonoBehaviour
{
    private ControlScene controlScene;
    public Fase tempFase;

    public int qtdEstrelasFase; // salvar valor e resgatar no start

    [SerializeField] private TextMeshProUGUI faseText;
    [SerializeField] private GameObject block;

    [Header("Config fase")]
    public int idTema;
    public bool isTransition;
    public int newIdTema;
    public int qtdCenarios;

    [Space(25)]

    public Transform avatarPinPos;

    public int idFase;
    public Sprite[] button;

    public Image btnImage;
    public Image estrelasFase;

    public GameObject requisito;
    public TextMeshProUGUI custoText;
    public int qtdCenouraCusto;

    public bool isBlock;
    
    public ScrollRect scrollRect;

    private void Initialization()
    {
        controlScene = FindObjectOfType<ControlScene>();
        tempFase = controlScene.fase;
        CheckBlock();


        estrelasFase.sprite = controlScene.stars[qtdEstrelasFase];

        scrollRect = FindObjectOfType<ScrollRect>();
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
            tempFase.idTema = idTema;
            tempFase.isTransition = isTransition;
            tempFase.newIdTema = newIdTema;
            tempFase.qtdCenarios = qtdCenarios;

            controlScene.avatarPin.transform.SetParent(transform);
            controlScene.avatarPin.transform.position = avatarPinPos.position;

            
        }
    }
}
