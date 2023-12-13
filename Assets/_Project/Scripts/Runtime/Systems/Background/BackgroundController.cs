using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public string sortingOrder;
    public int orderInLayer;

    private int idCenarioPrev;

    private Renderer mRenderer;

    private float offSet;
    public float speedOffSet;

    private Material currentMaterial;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        mRenderer = GetComponent<Renderer>();
        mRenderer.sortingLayerName = sortingOrder;
        mRenderer.sortingOrder = orderInLayer;
        currentMaterial = mRenderer.material;
        ChangeBackground();
    }

    private void Update()
    {
        if (idCenarioPrev != gameManager.idCenario)
        {
            ChangeBackground();
        }

        offSet += speedOffSet;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }

    private void ChangeBackground()
    {
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

        currentMaterial = mRenderer.material;

    }
}
