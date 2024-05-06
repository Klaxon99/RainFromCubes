using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeCreator _cubeCreator;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _minSpawnHeight;
    [SerializeField] private float _maxSpawnHeight;

    private Collider _spawnPlaceCollider;

    private void Start()
    {
        StartCoroutine(CubeSpawnRoutine());
    }

    private void Awake()
    {
        _spawnPlaceCollider = GetComponent<Collider>();
    }

    private IEnumerator CubeSpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            _cubeCreator.CreateCube(GetRandomSpawnPosition());

            yield return wait;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 planeBounds = _spawnPlaceCollider.bounds.extents;

        float randomX = Random.Range(-planeBounds.x, planeBounds.x);
        float randomY = Random.Range(_minSpawnHeight, _maxSpawnHeight);
        float randomZ = Random.Range(-planeBounds.z, planeBounds.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}