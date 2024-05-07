using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _cubeContainer;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(_cubePrefab, _cubeContainer);
    }

    public Cube CreateCube(Vector3 position)
    {
        Cube cube = _cubePool.GetItem();
        cube.gameObject.SetActive(true);
        cube.Initialize(Random.Range(_minLifeTime, _maxLifeTime),this);
        cube.transform.position = position;

        return cube;
    }

    public void ReturnCube(Cube cube)
    {
        _cubePool.AddItem(cube);
    }
}