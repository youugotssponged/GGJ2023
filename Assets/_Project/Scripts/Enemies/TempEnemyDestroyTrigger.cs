using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyDestroyTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider triggered.");
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy destroyed.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger triggered.");
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy destroyed.");
        }
    }
}
