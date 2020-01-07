using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : ManagerBase<InputManager>
{

    float f_xInput, f_zInput;
    bool b_jump;

    /// <summary>
    /// gets the inputs from the player
    /// </summary>
    private void Update()
    {
        b_jump = false;

        b_jump = Input.GetButtonDown("Jump");

        f_xInput = Input.GetAxisRaw("Horizontal");
        f_zInput = Input.GetAxisRaw("Vertical");

    }

    /// <summary>
    /// A function for other scripts to call to get a normalised V3 containing the x and z inputs from the player
    /// </summary>
    public Vector3 GetXZInputs()
    {
        return new Vector3(f_xInput, 0, f_zInput).normalized;
    }

    /// <summary>
    /// A function for other scripts to call to find if the jump button was pressed
    /// </summary>
    public bool GetJump()
    {
        return b_jump;
    }

}
