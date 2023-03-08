using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        if (transform.rotation.z > 360)
        {
            Vector3 rot = new Vector3(transform.rotation.x, transform.rotation.y, 0);
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}