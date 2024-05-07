using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private float _lifeTime;
    private CubeCreator _cubeCreator;
    private bool _hasCollision = false;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDisable()
    {
        _hasCollision = false;
        _meshRenderer.material.color = Color.gray;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out SpawnPlace spawnPlace))
        {
            if (_hasCollision == false)
            {
                float minColor = 0f;
                float maxColor = 1f;
                _meshRenderer.material.color = Color.Lerp(Color.red, Color.green, Random.Range(minColor, maxColor));
                _hasCollision = true;

                StartCoroutine(WaitLifeTimeRoutine());
            }
        }
    }

    public void Initialize(float lifeTime, CubeCreator cubeCreator)
    {
        _lifeTime = lifeTime;
        _cubeCreator = cubeCreator;
    }

    private IEnumerator WaitLifeTimeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_lifeTime);

        yield return wait;

        _cubeCreator.ReturnCube(this);
    }
}