using UnityEngine;

namespace PainfulSmile.Runtime.Utilities
{
    public class IsVisible : MonoBehaviour
    {
        public bool Visible { get; private set; }
        private void OnBecameVisible()
        {
            Visible = true;
        }
        private void OnBecameInvisible()
        {
            Visible = false;
        }
    }
}
