using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_snowball : Obstacle
{
    [SerializeField] float rotateSpeed;
    public override void UpdateMove()
    {
        base.UpdateMove();

        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
    }
}
