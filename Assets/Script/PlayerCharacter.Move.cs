using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerCharacter
{
    [SerializeField] private float movementSpeed;

    private void UpdateMove()
    {
        UpdateRotation();
    }

    //private void StopMoving()
    //{
    //    throw new NotImplementedException();
    //}

    private void Move(Vector3 movementDirection, float axisValue)
    {
        transform.Translate(movementDirection * movementSpeed * axisValue * Time.deltaTime, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), transform.position.y,transform.position.z);
    }

    private void UpdateRotation()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") >0 ){
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 45f, 0);
        }else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90f, 0);
        }else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 135f, 0);
        }else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180f, 0);
        }else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 225f, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 270f, 0);
        }else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 315f, 0);
        }
    }

}
