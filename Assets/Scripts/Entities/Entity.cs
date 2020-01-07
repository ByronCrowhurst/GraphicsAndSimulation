using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all entities
/// 
/// <param i_health="health">Health of entitiy</param>
/// <param rb="RigidBody">Health of entitiy</param>
/// </summary>
[RequireComponent(typeof(Rigidbody))]

abstract public class Entity : MonoBehaviour
{
    protected int i_health;
    protected Rigidbody rb;

    public virtual void Init()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Physics calculations happen here
    private void FixedUpdate()
    {
        
    }

    public virtual void ChangeHealth(int _delta)
    {
        i_health += _delta;
        if(i_health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
