using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Material _collisionMaterial;

    private MeshRenderer _meshRenderer;
    private float _lifeTime;
    private CubeCreator _cubeCreator;
    private bool _hasCollision = false;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(float lifeTime, CubeCreator cubeCreator)
    {
        _lifeTime = lifeTime;
        _cubeCreator = cubeCreator;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out SpawnPlace spawnPlace))
        {
            if (_hasCollision == false)
            {
                _meshRenderer.material = _collisionMaterial;
                StartCoroutine(WaitLifeTimeRoutine());
                _hasCollision = true;
            }
        }
    }

    private IEnumerator WaitLifeTimeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_lifeTime);

        yield return wait;

        _cubeCreator.DestroyCube(this);
    }
}