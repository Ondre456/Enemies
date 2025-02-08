using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 0.3f;

    private EnemyPool _enemyPool;
    private Vector3 _position;
    
    private void Awake()
    {
        _position = transform.position;
        _enemyPool = GetComponent<EnemyPool>();
        
        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        const int Compensator = 1;

        WaitForSeconds waitForSeconds = new WaitForSeconds(_repeatRate + Compensator);

        while (enabled)
        {
            Enemy newEnemy = _enemyPool.Get();
            newEnemy.transform.position = _position;
            GiveDirection(newEnemy);

            yield return waitForSeconds;
        }
    }

    private void GiveDirection(Enemy enemy)
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        enemy.AcceptDirection(randomDirection);
    }
}
