using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMirror : MonoBehaviour
{
    public Camera   originalCamera;
    
    Vector3  mirrorNormal;

    void Start()
    {
        mirrorNormal = transform.forward;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0.0f, originalCamera.transform.position.y, 0.0f);

        Vector3 toMirror = (transform.position - originalCamera.transform.position).normalized;

        Vector3 reflectedDir = Vector3.Reflect(toMirror, mirrorNormal);

        transform.rotation = Quaternion.LookRotation(reflectedDir, Vector3.up);
    }
}
