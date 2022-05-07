using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Set In Inspector")]
    public GameObject mainCamera;
    public float scrollSpeed = -5f;

    void Update()
    {
        Vector3 vec = transform.position;
        vec.x += scrollSpeed * Time.deltaTime;
        transform.position = vec;

        if (transform.position.x < mainCamera.transform.position.x - 111)
        {
            vec.x += 222;
            transform.position = vec;
        }
        else if (transform.position.x > mainCamera.transform.position.x + 111)
        {
            vec.x -= 444;
            transform.position = vec;
        }
    }
}
