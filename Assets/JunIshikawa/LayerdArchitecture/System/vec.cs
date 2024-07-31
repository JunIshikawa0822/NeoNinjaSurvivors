using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vec : MonoBehaviour
{
    public GameObject _object;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 restVec = Quaternion.Euler(0, 90, 0) * transform.forward;
        _object.transform.position = this.transform.position + new Vector3(0, 5, 0);

        Debug.DrawLine(this.transform.position, _object.transform.position, Color.red);
    }
}
