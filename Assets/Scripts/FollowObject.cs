using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject m_followTarget;

    [SerializeField] private bool m_doFollowX = true;
    [SerializeField] private bool m_doFollowY = true;
    [SerializeField] private bool m_doFollowZ = true;

    [SerializeField] private Vector3 m_offset = Vector3.zero;

    private void Update()
    {
        transform.position = new Vector3(
            m_doFollowX ? m_followTarget.transform.position.x + m_offset.x : transform.position.x,
            m_doFollowY ? m_followTarget.transform.position.y + m_offset.y : transform.position.y,
            m_doFollowZ ? m_followTarget.transform.position.z + m_offset.z : transform.position.z
        );
    }
}
