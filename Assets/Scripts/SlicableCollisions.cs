using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicableCollisions : MonoBehaviour
{
    bool sliced;
    private void Awake()
    {
        if (!this.gameObject.CompareTag(Tags.slicableTag))
        {
            this.gameObject.tag = Tags.slicableTag;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tags.knifeTag) && !sliced)//&& collision.collider is BoxCollider)
        {
            if (collision.collider is BoxCollider)
                KnifeMovement.Instance.GoBackJump();
            else if (collision.collider is CapsuleCollider)
            {
                Slicer.Instance.SliceMe(this.gameObject);
                sliced = true;
            }

        }
    }


}
