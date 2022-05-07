using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject particle;

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            GameObject par = Instantiate(particle);
            par.transform.position = this.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go.tag == "HeroProjectile")
        {
            Destroy(go);
        }
    }
}
