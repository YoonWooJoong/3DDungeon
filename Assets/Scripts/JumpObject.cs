using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>()!=null)
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*400f,ForceMode.Impulse);
    }
}
