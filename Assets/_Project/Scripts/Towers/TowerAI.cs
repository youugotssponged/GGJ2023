using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TowerAI : MonoBehaviour
{
    private List<GameObject> EnemiesWithinRadius;
    private ITower TowerData;
    private bool InCoolDown;

    public AudioSource _Source;

    private void Awake()
    {
        EnemiesWithinRadius = new List<GameObject>();
        TowerData = GetComponent<ITower>();
        _Source = GetComponent<AudioSource>();
    }
    private IEnumerator TriggerCoolDown()
    {
        InCoolDown = true;
        yield return new WaitForSeconds(TowerData.CoolDownTimeInSeconds);
        InCoolDown = false;
    }

    void Update()
    {
        EnemiesWithinRadius.RemoveAll(x => x == null);
        if (EnemiesWithinRadius.Count > 0)
        {
            var selectedEnemy = EnemiesWithinRadius.FirstOrDefault();
            if(selectedEnemy != null) 
            { 
                gameObject.transform.LookAt(selectedEnemy.transform);
                if (!InCoolDown)
                {
                    var nav = selectedEnemy.GetComponent<NavMeshAgent>();
                    foreach (var enemy in EnemiesWithinRadius)
                    {
                        if (enemy != null)
                        {

                            var navOfEnemy = enemy.GetComponent<NavMeshAgent>();
                            if (navOfEnemy.remainingDistance < nav.remainingDistance)
                            {
                                selectedEnemy = enemy;
                            }
                        }
                    }


                    var instantiated = Instantiate(TowerData.EffectOnEnemy, selectedEnemy.transform.parent, true);
                    instantiated.transform.position = selectedEnemy.transform.position;

                    _Source.PlayOneShot(TowerData.ShootSound);
                    selectedEnemy.GetComponent<IEnemy>().TakeDamage(TowerData.Damage);
                    StartCoroutine(nameof(TriggerCoolDown));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (!EnemiesWithinRadius.Contains(other.gameObject))
            {
                EnemiesWithinRadius.Add(other.gameObject);
                Debug.Log("Enemy ADDED");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (EnemiesWithinRadius.Contains(other.gameObject))
            {
                EnemiesWithinRadius.Remove(other.gameObject);
                Debug.Log("Enemy Removed");
            }
        }
    }
}
