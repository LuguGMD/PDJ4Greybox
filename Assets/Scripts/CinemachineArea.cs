using UnityEngine;

public class CinemachineArea : MonoBehaviour
{
    [SerializeField] GameObject m_cinemachineCamera;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_cinemachineCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_cinemachineCamera.SetActive(false);
        }
    }
}
