using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private WaitForSeconds _lifeTimeWait;
    private bool _hasCollision;
    private CubeSpawner _spawner;

    public float LifeTime { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDisable()
    {
        ResetState();
    }

    public void Initialize(float lifeTime, CubeSpawner spawner)
    {
        _spawner = spawner;
        LifeTime = lifeTime;
        _lifeTimeWait = new WaitForSeconds(LifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollision == false)
        {
            if (collision.collider.TryGetComponent(out Platform platform))
            {
                _hasCollision = true;
                float minColor = 0f;
                float maxColor = 1f;
                _meshRenderer.material.color = Color.Lerp(Color.black, Color.white, Random.Range(minColor, maxColor));

                StartCoroutine(WaitLifeTimeRoutine());
            }
        }
    }

    private IEnumerator WaitLifeTimeRoutine()
    {
        yield return _lifeTimeWait;

        _spawner.Return(this);
    }

    private void ResetState()
    {
        _hasCollision = false;
        _meshRenderer.material.color = Color.gray;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}