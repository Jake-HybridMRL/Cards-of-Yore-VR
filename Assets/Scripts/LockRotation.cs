using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    Quaternion initialRot;
    // Start is called before the first frame update
    void Start(){}

    void Awake()
    {
        initialRot=transform.rotation;
    }

    void LateUpdate(){
        transform.rotation = initialRot;
}

    // Update is called once per frame
    void Update()
    {
    }
}
