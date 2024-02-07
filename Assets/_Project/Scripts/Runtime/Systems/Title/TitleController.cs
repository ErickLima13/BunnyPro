using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    private AudioController audioController;

    [SerializeField] private Button buttonPlay;

    [SerializeField] private Sprite[] spriteMusic;
    [SerializeField] private Sprite[] spriteFx;

    [SerializeField] private Image buttonMusic;
    [SerializeField] private Image buttonFx;

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
        audioController.TrocarCena("Map", true, audioController.map);
    }

    private void CheckAudioButtons()
    {
        buttonMusic.sprite = spriteMusic[1];
        buttonFx.sprite = spriteFx[1];

        if (audioController.music.mute)
        {
            buttonMusic.sprite = spriteMusic[0];
        }

        if (audioController.fx.mute)
        {
            buttonFx.sprite = spriteFx[0];
        }
    }

    public void MuteButton(bool isMusic)
    {
        if (isMusic)
        {
            audioController.MuteMusic();
        }
        else
        {
            audioController.MuteFx();
        }

        CheckAudioButtons();
    }
}
