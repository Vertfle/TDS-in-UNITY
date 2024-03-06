using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null )
        {
            enemyHealth.value -= damage;
            if (enemyHealth.value <= 0 )
            {
                Destroy(enemyHealth.gameObject);
            }    
        }
        DestroyFireball();
    }
    void Start()
    {
        Invoke("DestroyFireball", lifetime);
    }
    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * transform.forward;

    }
    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
