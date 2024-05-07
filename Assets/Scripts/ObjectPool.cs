using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Queue<T> _items;
    private Transform _container;

    public ObjectPool(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container; 
        _items = new Queue<T>();
    }

    public T GetItem()
    {
        if (_items.Count > 0)
        {
            return _items.Dequeue();
        }

        return CreateItem();
    }

    public void AddItem(T item)
    {
        _items.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    private T CreateItem()
    {
        T item = GameObject.Instantiate(_prefab, _container);
        item.gameObject.SetActive(false);

        return item;
    }
}