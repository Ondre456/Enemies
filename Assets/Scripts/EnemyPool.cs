using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _maxPoolSize = 10;
    [SerializeField] private int _poolCapacity = 5;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
                createFunc: () => CreateFunction(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _maxPoolSize
            );
    }

    public Enemy Get()
    {
        return _pool.Get();
    }

    private Enemy CreateFunction()
    {
        Enemy instance = Instantiate(_prefab);

        if (instance.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = true;
        }

        if (instance.TryGetComponent<Collider>(out Collider collider))
        {
            collider.enabled = true;
        }

        return instance;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.TryGetComponent(out Repainter repainter);
        repainter.SetDefaultColor();
        enemy.Deactivated += OnEnemyDeactivated;
    }

    private void OnEnemyDeactivated(Enemy enemy)
    {
        enemy.Deactivated -= OnEnemyDeactivated;
        _pool.Release(enemy);
    }
}
