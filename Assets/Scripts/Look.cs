using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        var mousePosition = Input.mousePosition;
        var worldPosition = cam.ScreenToWorldPoint(mousePosition);
        transform.LookAt(worldPosition, Vector3.forward);
    }
}
