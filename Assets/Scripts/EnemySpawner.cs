using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 0.3f;
    [SerializeField] private Goal _goal;

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
            newEnemy.AcceptGoal(_goal);

            yield return waitForSeconds;
        }
    }
}
