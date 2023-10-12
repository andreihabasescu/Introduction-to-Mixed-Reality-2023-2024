using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    [SerializeField] float speed;
    public void Rotate() {
        //transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    /*private void Update()
    {
        Rotate();
    }*/
}
