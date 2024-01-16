using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    private AudioController audioController;

    public Sprite[] stars;

    public int qtdCenouras;

    public GameObject avatarPin;
   
    void Start()
    {
        audioController = FindObjectOfType<AudioController>();

        if (audioController == null)
        {
            SceneManager.LoadScene(0);
            return;
        }

        audioController.FadeOut();
    }

    public void BtnFase()
    {
        audioController.TrocarCena("0", true, audioController.fase);
    }


}
