using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject particle;
    [Header("Set In Inspecter")]
    public float projectileLifetime = 3f;

    void Start()
    {
        Invoke("DestroyProjectile", projectileLifetime);
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            GameObject par = Instantiate(particle);
            par.transform.position = this.transform.position;
        }  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
