using UnityEngine;

namespace PDJ4.Observer
{
    public interface IObserver
    {
        public void Notify(Color c);
    }
}
