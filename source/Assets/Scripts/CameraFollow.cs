using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmootheness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform carTarget;

    public void FixedUpdate()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        HandleMovement();
        HandleRotation(); 
    }
    private void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position =Vector3.Lerp(transform.position, targetPos, moveSmootheness * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = carTarget.position- transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}
