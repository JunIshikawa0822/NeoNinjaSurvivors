using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 restVec = Quaternion.Euler(0, 90, 0) * transform.forward;
        Vector3 u = this.gameObject.transform.position + new Vector3(0, 5, 0) * 30;
        Vector3 i = Quaternion.Euler(0, 90, 0) * u;
        Debug.DrawLine(this.gameObject.transform.position, i, Color.red);
    }
}
