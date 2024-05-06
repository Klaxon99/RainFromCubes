using UnityEngine;

public class CubeCreator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    public Cube CreateCube(Vector3 position)
    {
        Cube cube = Instantiate(_cubePrefab, position, Quaternion.identity);
        cube.Initialize(Random.Range(_minLifeTime, _maxLifeTime), this);

        return cube;
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}