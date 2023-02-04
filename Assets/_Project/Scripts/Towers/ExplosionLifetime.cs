using System.Collections;
using UnityEngine;

public class ExplosionLifetime : MonoBehaviour
{
    private ParticleSystem parts;
    private float duration;

    void Start()
    {
        parts = GetComponent<ParticleSystem>();
        duration = parts.main.duration;
        StartCoroutine(nameof(SelfDestruct));
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
