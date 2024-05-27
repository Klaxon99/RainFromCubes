using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _containter;
    [SerializeField] private ObjectsCountView _objectsCountView;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, _containter);
        _objectsCountView.Initialize(_pool);
    }

    public virtual void Return(T spawnObject)
    {
        _pool.AddItem(spawnObject);
    }

    public T Spawn(Vector3 spawnPosition)
    {
        T spawnObject = _pool.GetItem();
        spawnObject.gameObject.SetActive(true);
        spawnObject.transform.position = spawnPosition;

        return spawnObject;
    }
}