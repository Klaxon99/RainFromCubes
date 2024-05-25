using System.Collections;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent(typeof(Exploder))]
public class Bomb : MonoBehaviour
{
    private float _detonateDelay;
    private MeshRenderer _meshRenderer;
    private Exploder _exploder;
    private BombSpawner _bombSpawner;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _exploder = GetComponent<Exploder>();
    }

    public void Initialize(BombSpawner bombSpawner, float detonateDelay)
    {
        _bombSpawner = bombSpawner;
        _detonateDelay = detonateDelay;

        StartCoroutine(DetonateRoutine());
    }

    private IEnumerator DetonateRoutine()
    {
        float timer = 0f;
        Color baseColor = _meshRenderer.material.color;

        while (timer < _detonateDelay)
        {
            timer += Time.deltaTime;
            baseColor.a = 1 - (timer / _detonateDelay);
            _meshRenderer.material.color = baseColor;

            yield return null;
        }

        _exploder.Detonate(transform.position);
        _bombSpawner.Return(this);
    }
}