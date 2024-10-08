using UnityEngine;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour
{
    [SerializeField] private ButtonFase[] buttonFase;
    [SerializeField] private int totalFases;

    private void Start()
    {
        SetupMap();
    }

    private void SetupMap()
    {
        foreach (ButtonFase b in buttonFase)
        {
            b.SetTextNumber(totalFases.ToString());
            b.idFase = totalFases;
            totalFases++;
        }
    }
}
