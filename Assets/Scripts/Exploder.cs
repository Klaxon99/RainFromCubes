using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public void Detonate(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.attachedRigidbody != null)
            {
                collider.attachedRigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }
        }
    }
}