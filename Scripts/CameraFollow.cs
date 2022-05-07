using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static public GameObject POI;
    [Header("Set In Inspector")]
    public float easing = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        POI = GameObject.FindGameObjectWithTag("Hero");
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = POI.transform.position.x;
        pos.y = 20;

        pos.x += 25; //Offset camera slightly

        pos = Vector3.Lerp(transform.position, pos, easing);
        transform.position = pos;
    }
}
