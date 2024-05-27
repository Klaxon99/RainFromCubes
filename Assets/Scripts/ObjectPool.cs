using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectsCounter where T : MonoBehaviour
{
    private T _prefab;
    private Queue<T> _queue;
    private int _minCount;
    private Transform _container;
    private int _creatingObjectsCount;
    private int _activeObjectsCount;

    public event Action<int> CreatingObjectsCountChanged;
    public event Action<int> ActiveObjectsCountChanged;

    public ObjectPool(T prefab, Transform container)
    {
        _prefab = prefab;
        _queue = new Queue<T>();
        _minCount = 0;
        _container = container;
    }

    public int CreatingObjectsCount
    {
        get
        {
            return _creatingObjectsCount;
        }

        private set
        {
            ChangeObjectsCount(value, CreatingObjectsCountChanged, ref _creatingObjectsCount);
        }
    }
    public int ActiveObjectsCount
    {
        get
        {
            return _activeObjectsCount;
        }

        private set
        {
            ChangeObjectsCount(value, ActiveObjectsCountChanged, ref _activeObjectsCount);
        }
    }

    public T GetItem()
    {
        ActiveObjectsCount++;
        
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

        ActiveObjectsCount--;

        _queue.Enqueue(poolObject);
    }

    private T CreateItem()
    {
        T item = GameObject.Instantiate(_prefab);
        item.gameObject.SetActive(false);
        item.transform.SetParent(_container);

        CreatingObjectsCount++;

        return item;
    }

    private void ChangeObjectsCount(int value, Action<int> countChangeEvent, ref int objectsCount)
    {
        objectsCount = value;
        countChangeEvent?.Invoke(objectsCount);
    }
}