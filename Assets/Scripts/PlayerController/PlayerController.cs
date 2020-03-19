using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float maxSpeed;
    [SerializeField] private ParticleSystem hitPart;
    private Vector3 movement;
    Vector3 hitPoint;
    private float fire;
    private Rigidbody rb;
    bool cast;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        if (movement != Vector3.zero)
        {
            rb.AddForce((movement * Time.deltaTime) * offset, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        }
        else
        {
            rb.AddForce(-rb.velocity);
            rb.velocity = Vector3.zero;
        }
        if (fire == 1)
            CreateFireball(Random.Range(-0.5f, 0.5f),Random.Range(0.0f, 0.3f), Random.Range(0.0f, 10.0f));
        Ray pointer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(pointer.origin, pointer.direction, out hit, 500f, LayerMask.GetMask("Ground")))
        {
            hitPoint = hit.point;
        }
        Vector3 lookTarget = hitPoint - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500);
    }

    void GetInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        fire = Input.GetAxisRaw("Fire1");
    }

    void CreateFireball(float ramp, float amp, float force)
    {
        if (!cast)
        {
            GameObject fireball = new GameObject();
            fireball.layer = 11;
            Sphere sph = fireball.AddComponent<Sphere>();
            sph.RampValue = ramp;
            sph.AplitudeValue = amp;
            sph.Force = force;
            sph.Direction = transform.forward;
            sph.HitPart = hitPart;
            fireball.transform.position = transform.position;
            StartCoroutine("Cooldown");
        }
    }

    IEnumerator Cooldown()
    {
        cast = true;
        yield return new WaitForSecondsRealtime(1);
        cast = false;
        StopCoroutine("Cooldown");
    }
}
