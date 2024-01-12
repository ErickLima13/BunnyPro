using UnityEngine;

public class MapControl : MonoBehaviour
{
    private AudioController audioController;

    [SerializeField] private ButtonFase[] buttonFase;
    [SerializeField] private int totalFases;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        audioController.FadeOut();
        SetupMap();
    }

    private void SetupMap()
    {
        foreach (ButtonFase b in buttonFase)
        {
            b.SetTextNumber(totalFases.ToString());
            totalFases++;
        }
    }


}
