using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    private AudioController audioController;
    public Fase fase;

    public Sprite[] stars;

    public int qtdCenouras;

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
    }

    public void BtnFase()
    {
        audioController.TrocarCena("0", true, audioController.fase);
    }


}
