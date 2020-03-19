using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Transform target;
    public Transform Target { set { target = value; } }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (transform.position - target.position).normalized;
        rb.AddForce(-(direction * Time.deltaTime) * speed, ForceMode.Impulse);

    }

}
