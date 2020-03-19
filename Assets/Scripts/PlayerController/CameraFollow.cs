using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float y = 15;
    [SerializeField] Transform target;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, y, target.transform.position.z);
    }
}
