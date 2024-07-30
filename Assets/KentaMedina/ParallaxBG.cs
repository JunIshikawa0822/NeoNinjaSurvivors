using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    Transform camT;
    [SerializeField] private float scrollSpeed = -0.2f;
    // Start is called before the first frame update
    void Start()
    {
        camT = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(camT.position.x * scrollSpeed, transform.position.y, camT.position.z * scrollSpeed);

    }
}
