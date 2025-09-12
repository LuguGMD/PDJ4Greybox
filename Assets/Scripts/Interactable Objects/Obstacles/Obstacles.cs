using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe base para obstáculos que detectam colisões via Trigger.
/// Fornece implementação básica de filtros de colisão.
/// </summary>
public class Obstacles : MonoBehaviour, IColliderEnterCollision
{
    public GameObject player;
    public LayerMask CollisionMask { get; set; }
    public HashSet<string> CollisionTags { get; set; }

    public Vector3 playerStartPos;

    void Start()
    {
        playerStartPos = player.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Teleporte solicitado. Antes={other.transform.position}");

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; 
                other.transform.position = playerStartPos;
                cc.enabled = true;  
            }
            else
            {
                other.transform.position = playerStartPos;
            }

            Debug.Log($"Depois={other.transform.position}");
        }
    }
}
