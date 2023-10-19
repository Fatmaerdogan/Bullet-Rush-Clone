using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class ShootArea : MonoBehaviour
{
    [SerializeField] bool LocationStatus;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag($"Enemy"))
        {
            Events.EnemiesAdd?.Invoke(LocationStatus, other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag($"Enemy"))
        {
            Events.EnemiesRemove?.Invoke(LocationStatus, other.gameObject);
        }
    }
}
