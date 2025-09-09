using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PDJ4.Observer
{
    public class Subject : MonoBehaviour
    {
        private List<IObserver> observers = new List<IObserver>();
        private static Subject m_instance;
        public static Subject Instance
        {
            get
            {
                return m_instance;
            }
        }

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyAll(Color color)
        {

            foreach (IObserver observer in observers)
            {
                observer.Notify(color);
            }
        }
    }
}
