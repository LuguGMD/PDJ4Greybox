using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base para obstáculos que detectam colisões via Trigger.
/// Fornece implementação básica de filtros de colisão.
/// </summary>
public class Obstacles : MonoBehaviour, IColliderEnterCollision
{
    public LayerMask CollisionMask { get; set; }
    public HashSet<string> CollisionTags { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Teleporte solicitado. Antes={other.transform.position}");
            if (other.TryGetComponent<PlayerSpawnpoint>(out PlayerSpawnpoint playerSpawnpoint))
            {
                playerSpawnpoint.ReturnToSpawnpoint();
                GameManager.Instance.PlayerDied();
            }
            Debug.Log($"Depois={other.transform.position}");
        }
    }
}
