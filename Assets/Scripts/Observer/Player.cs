using UnityEngine;

namespace PDJ4.Observer
{
    public class Player : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Subject.Instance.NotifyAll(Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Subject.Instance.NotifyAll(new Color(Random.value, Random.value, Random.value));
            }
        }
    }
}
