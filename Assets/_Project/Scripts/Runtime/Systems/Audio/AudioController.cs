using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private IsDone isDone;

    [Header("Audio Controller")]
    public AudioSource music;
    public AudioSource fx;

    [Header("Musicas")]
    public AudioClip title;
    public AudioClip map;
    public AudioClip fase;
    public AudioClip gameOver;

    [Header("Fx")]
    public AudioClip coletavel;

    public Animator fadeAnimator;

    private string novaCena;
    private AudioClip novaMusica;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        isDone = FindObjectOfType<IsDone>();
    }

    private void Start()
    {
        music.clip = title;
        music.loop = true;
        music.Play();

        LoadNewScene("Title");
    }

    public void TrocarCena(string nomeCena, bool isMudarMusica, AudioClip musica)
    {
        switch (isMudarMusica)
        {
            case true:

                novaCena = nomeCena;
                novaMusica = musica;

                StartCoroutine(nameof(MudarCenaMusica));
                break;
            case false:
                SceneManager.LoadScene(nomeCena);
                break;
        }
    }

    private IEnumerator MudarCenaMusica()
    {
        FadeIn();
        float volumeMaximo = music.volume;

        for (float volume = music.volume; volume > 0; volume -= 0.01f)
        {
            music.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        music.clip = novaMusica;
        music.Play();

        yield return new WaitUntil(() => isDone.isFinish);

        LoadNewScene(novaCena);

        for (float volume = 0; volume < volumeMaximo; volume += 0.01f)
        {
            music.volume = volume;
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }

    public void LoadNewScene(string nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void FadeIn()
    {
        isDone.isFinish = false;
        fadeAnimator.SetTrigger("fadeIn");
    }

    public void FadeOut()
    {
        isDone.isFinish = false;
        fadeAnimator.SetTrigger("fadeOut");
    }
}
