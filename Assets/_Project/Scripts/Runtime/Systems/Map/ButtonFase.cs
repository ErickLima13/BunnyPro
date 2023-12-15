using TMPro;
using UnityEngine;

public class ButtonFase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI faseText;
    [SerializeField] private GameObject block;

    private bool isBlock;

    public void SetBlock(bool value)
    {
        isBlock = value;
        block.SetActive(isBlock);
    }

    public void SetTextNumber(string number)
    {
        faseText.text = number;
    }
}
