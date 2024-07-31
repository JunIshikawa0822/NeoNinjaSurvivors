using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ImpulseTestScript : MonoBehaviour
{
    private float magnitude = 1f;
    [SerializeField] private CinemachineImpulseSource myCIS;
    [SerializeField] private Transform followTransf;
    private float nextImpulseTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if(!followTransf)
        {
            followTransf = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextImpulseTime)
        {
            myCIS.GenerateImpulse();
            nextImpulseTime += 1f;
        }
    }

    private void createDirectionalImpulse()
    {
        Vector3 offset = followTransf.position - transform.position;
        offset.y = 0f;
        Vector3 vel = offset.normalized * magnitude;
        myCIS.GenerateImpulseWithVelocity(vel);
    }
}
