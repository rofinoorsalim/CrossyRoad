using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepText;
    [SerializeField] ParticleSystem dieParticles;

    [SerializeField, Range(0.01f,1f)] private float moveDuration = 0.4f;
    [SerializeField, Range(0.01f, 1f)] private float jumpHeight = 0.5f;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;

    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel;}

    [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel; }

    public bool isDie { get => this.enabled == false;}

    public void SetUp(int minZpos, int extent)
    {
        backBoundary = minZpos;
        leftBoundary = -(extent+ 1);
        rightBoundary = extent + 1;
    }

    private void Update()
    {
        var moveDir  = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += new Vector3(0, 0, 1);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += new Vector3(0 , 0, -1);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += new Vector3(1, 0, 0);
        }

        if (moveDir == Vector3.zero)
        {
            return;
        }

        if (isJumping() == false)
        {
            Jump(moveDir);
        }
    }

    private void Jump(Vector3 targetDirection)
    {
        SoundManager.instance.JumpingSound();
        //Atur rotasi
        Vector3 targetPosition = transform.position + targetDirection;
        transform.LookAt(targetPosition);

        //loncat
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration/2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration/2));

        if (targetPosition.z <= backBoundary || targetPosition.x <= leftBoundary || targetPosition.x >= rightBoundary)
        {
            return;
        }
        if (Tree.AllPositions.Contains(targetPosition))
        {
            return;
        }

        //maju mundur samping
        transform.DOMoveX(targetPosition.x, moveDuration);
        transform.DOMoveZ(targetPosition.z, moveDuration).OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int) this.transform.position.z;
        if (currentTravel > maxTravel)
        {
            maxTravel = currentTravel;
        }

        stepText.SetText("Step : " + maxTravel.ToString());
    }

    private bool isJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false)
        {
            return;
        }
        if(other.tag == "Car")
        {
            AnimateCrash();
        }
    }

    private void AnimateCrash()
    {
        SoundManager.instance.CrashSound();
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(2f, 0.2f);

        this.enabled = false;
        dieParticles.Play();
    }
}
