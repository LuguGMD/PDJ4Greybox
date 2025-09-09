using UnityEngine;

namespace PDJ4.Observer
{
    public class ObserverNPC : MonoBehaviour, IObserver
    {
        private MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            Subject.Instance.RegisterObserver(this);
        }
        public void Notify(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}
