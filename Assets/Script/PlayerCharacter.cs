using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerCharacter : Unit
{
    public override void Initialize()
    {
        state = UnitState.Idle;
    }

    private void Update()
    {
        UpdateInput();
        UpdateMove();
    }

    private void UpdateInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Move(Vector3.right, Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            Move(Vector3.forward, Input.GetAxisRaw("Vertical"));
        }
        //if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        //{
        //    StopMoving();
        //}

    }
}
