using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class GroundCollisions : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider is CapsuleCollider)
        {
            if (KnifeMovement.Instance.stopWatch.Elapsed.TotalSeconds > .5f)
            {
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

        }
    }
}
