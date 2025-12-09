using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;
    
    [SerializeField] private Vector2 initalShotVelocity = Vector2.zero;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Projectile projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initalShotVelocity == Vector2.zero)
        {
            initalShotVelocity = new Vector2(10f, 0f);
            Debug.LogWarning("Shoot: Initial shot velocity noaultingt set on Shoot of " + gameObject.name + ",defulting to " + initalShotVelocity);
        }

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab)
        {
            Debug.LogError("Shoot: Spawn points not set on Shoot of " + gameObject.name);
        }
    }

    public void Fire()
    {
        Projectile curProjectile;

        if (sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.setVelocity(initalShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.setVelocity(initalShotVelocity);
        }


    }

}
