using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Vector3 rotate;
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(rotate);
    }
    //private void Update()
    //{
        
    //    transform.Rotate(rotate * Time.deltaTime);
    //}
}
