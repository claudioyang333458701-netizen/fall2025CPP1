using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField, Range(0.5f, 10)] private float lifetime = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //after 'lifetime' seconds, destroy this projectile
    void Start() => Destroy(gameObject, lifetime);
    public void setVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;
}
