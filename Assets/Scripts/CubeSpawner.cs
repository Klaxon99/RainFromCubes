using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private Collider _spawnPlace;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    public override void Return(Cube cube)
    {
        Bomb bomb = _bombSpawner.Spawn(cube.transform.position);

        bomb.Initialize(_bombSpawner, cube.LifeTime);

        base.Return(cube);
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
        int minLifeTime = 2;
        int maxLifeTime = 5;

        while(enabled)
        {
            Cube cube = Spawn(GetRandomSpawnPosition());
            cube.Initialize(Random.Range(minLifeTime, maxLifeTime), this);

            yield return wait;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPlaceSize = _spawnPlace.bounds.extents;
        float spawnPositionX = Random.Range(-spawnPlaceSize.x, spawnPlaceSize.x);
        float spawnPositionZ = Random.Range(-spawnPlaceSize.z, spawnPlaceSize.z);

        return new Vector3(spawnPositionX, _spawnHeight, spawnPositionZ);
    }
}