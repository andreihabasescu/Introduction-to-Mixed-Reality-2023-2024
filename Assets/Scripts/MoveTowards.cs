using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 1.0f;

    private int number = 0;

    private Vector3 center;

    private Animator animator;

    private void Awake()
    {
        center = ARTapToPlace.getCenter();
    }
    public void setNumber(int num)
    {
        number = num;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void move()
    {
        var step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, calcCoords(number, ARTapToPlace.getRadius()), step);
    }

    public Vector3 calcCoords(int number, double radius)
    {
        double theta = (2 * Math.PI) / ARTapToPlace.getObjCount() * number;

        var x = Math.Cos(theta) * radius;
        var y = Math.Sin(theta) * radius;

        return center + new Vector3((float)x, 0, (float)y);
    }

    private void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, center);
        if ((distance < (ARTapToPlace.getRadius()+0.05f)) && number != 0)
        {
            move();
            animator.SetBool("HasReachedRadius",true);
        }
        else {
            move();
            animator.SetBool("HasReachedRadius", false);
        }
    }
}
