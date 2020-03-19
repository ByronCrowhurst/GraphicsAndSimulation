using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Enemy spawn;
    [SerializeField] Transform target;
    [SerializeField] float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0.0f, spawnTime);
        spawn.Target = target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(spawn);
        if (spawnTime >= 0.5)
            spawnTime -= 0.5f;
    }
}
