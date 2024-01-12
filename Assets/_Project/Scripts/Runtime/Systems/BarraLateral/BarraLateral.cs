using UnityEngine;

public class BarraLateral : MonoBehaviour
{

    public Animator barraAnimator;

    private bool isExpandido;
    private bool isMoving;
    

    public void Expandir()
    {
        if (!isMoving)
        {
            isMoving = true;
            isExpandido = !isExpandido;

            switch (isExpandido)
            {
                case true:
                    barraAnimator.SetTrigger("expandir");
                    break;
                case false:
                    barraAnimator.SetTrigger("recolher");
                    break;
            }
        }
    }


    public void SetIsMoving()
    {
        isMoving = false;
    }


}
