using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Sphere : MonoBehaviour
{
    [Range(2, 256)] public int resolution = 10;
    [SerializeField] float scale;
    [SerializeField, HideInInspector] MeshFilter[] meshFilters;
    [SerializeField] List<Material> materials = new List<Material>();
    [SerializeField, Range(-0.5f, 0.5f)] float rampValue;
    [SerializeField, Range(0.0f, 0.3f)] float amplitudeValue;
    ParticleSystem hitPart;
    SphereFace[] faces;
    Rigidbody rb;
    Collider col;
    float force;
    Vector3 direction;

    public ParticleSystem HitPart {set{ hitPart = value; }}

    public float RampValue { set { rampValue = value; } }
    public float AplitudeValue { set { amplitudeValue = value; } }
    public float Force { set { force = value; } } 
    public Vector3 Direction {  set{ direction = value; } }

    void Start()
    {
        Initialize();
        GenerateMesh();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        Invoke("Die", 5f);
    }

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    private void Update()
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitPart.transform.position = collision.transform.position;
            hitPart.Play();
            Destroy(col.gameObject);
            Destroy(collision.gameObject);

        }
        
    }

    void Initialize()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        faces = new SphereFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                Material fireBallMatreial = new Material(Shader.Find("Custom/Fireball"));
                meshObj.AddComponent<MeshRenderer>().material = fireBallMatreial;
                fireBallMatreial.SetFloat("_RampVal", rampValue);
                fireBallMatreial.SetFloat("_Amplitude", amplitudeValue);
                
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                materials.Add(fireBallMatreial);
                meshFilters[i].sharedMesh = new Mesh();
            }
            faces[i] = new SphereFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        int sphereCount = 0;
        foreach (SphereFace sphere in faces)
        {

            sphere.CreateMesh();
        }

    }

    void Die()
    {
        Destroy(this);
    }
}
