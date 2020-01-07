using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : Mobile
{
    // Start is called before the first frame update
    protected InputManager im;
    void Start()
    {
        im = GetComponent<InputManager>();
        Debug.Log(im);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
