using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public enum Cena
    {
        Gameplay,
        Titulo
    }

    public string sortingOrder;
    public int orderInLayer;

    private int idCenarioPrev;

    private Renderer mRenderer;

    private float offSet;
    private float speedController;
    private float prevCamX;

    public float speedOffSet;

    private Material currentMaterial;
    private GameManager gameManager;

    public Cena cenaAtual;

    private void Start()
    {
        mRenderer = GetComponent<Renderer>();
        mRenderer.sortingLayerName = sortingOrder;
        mRenderer.sortingOrder = orderInLayer;
        currentMaterial = mRenderer.material;

        if (IsGameplay())
        {
            gameManager = GameManager.Instance;
            idCenarioPrev = gameManager.idCenario;
            ChangeBackground();
        }

    }

    private void Update()
    {
        if (IsGameplay())
        {
            if (idCenarioPrev != gameManager.idCenario)
            {
                ChangeBackground();
            }

            float horizontalPlayer = gameManager.player.horizontal;
            if (horizontalPlayer == 0)
            {
                speedController = 0f;
            }
            else
            {
                if (horizontalPlayer > 0)
                {
                    speedController = 1;
                }
                else
                {
                    speedController = -1;
                }
            }

            if (gameManager.cam.transform.position.x != prevCamX)
            {
                offSet += speedOffSet * speedController;
                currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
            }
        }
        else
        {
            offSet += speedOffSet;
            currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
        }
    }

    private void LateUpdate()
    {
        if (IsGameplay())
        {
            prevCamX = gameManager.cam.transform.position.x;
        }
    }

    private void ChangeBackground()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        idCenarioPrev = gameManager.idCenario;

        switch (orderInLayer)
        {
            case 0:
                mRenderer.material = gameManager.BG0[idCenarioPrev];
                break;
            case 1:
                mRenderer.material = gameManager.BG1[idCenarioPrev];
                break;
            case 2:
                mRenderer.material = gameManager.BG2[idCenarioPrev];
                break;
            case 3:
                mRenderer.material = gameManager.BG3[idCenarioPrev];
                break;
        }

        gameManager.cam.backgroundColor = gameManager.skyColor[idCenarioPrev];
        currentMaterial = mRenderer.material;

    }

    private bool IsGameplay()
    {
        return cenaAtual == Cena.Gameplay;
    }
}
