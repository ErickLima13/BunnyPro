using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private AudioController audioController;

    [SerializeField] private Button buttonPlay;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController>();
        buttonPlay.onClick.AddListener(PlayButton);


        if (audioController == null)
        {
            SceneManager.LoadScene(0);
            return;
        }

      
        audioController.FadeOut();
    }


    public void PlayButton()
    {
        audioController.TrocarCena("Map",true,audioController.map);
    }
}
