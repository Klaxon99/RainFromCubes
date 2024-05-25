using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Queue<T> _queue;
    private int _minCount;
    private Transform _container;

    public ObjectPool(T prefab, Transform container)
    {
        _prefab = prefab;
        _queue = new Queue<T>();
        _minCount = 0;
        _container = container;
    }

    public T GetItem()
    {
        if (_queue.Count == _minCount)
        {
            return CreateItem();
        }
        else
        {
            return _queue.Dequeue();
        }
    }

    public void AddItem(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _queue.Enqueue(poolObject);
    }

    private T CreateItem()
    {
        T item = GameObject.Instantiate(_prefab);
        item.gameObject.SetActive(false);
        item.transform.SetParent(_container);

        return item;
    }
}