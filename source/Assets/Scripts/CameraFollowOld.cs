using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOld : MonoBehaviour
{
    public Transform Target;

    public Vector3 offset;
    public Vector3 eulerRotation;
    public float damper;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = eulerRotation;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Target == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position,Target.position+offset,damper*Time.deltaTime);
    }
}
