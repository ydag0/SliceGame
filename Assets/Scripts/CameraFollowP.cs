using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowP : MonoBehaviour
{
    #region Variables
    Transform cameraTransform;
    Transform knifeTransform;
    Vector3 offset;
    Vector3 velocity=Vector3.zero;
    Vector3 targetPos;
    #endregion
    private void Awake()
    {
        cameraTransform = Camera.main.gameObject.transform;
        knifeTransform = GameObject.FindGameObjectWithTag(Tags.knifeTag).transform;
        CalculateDistance();
    }
    //private void LateUpdate()
    //{
    //    targetPos = knifeTransform.position + offset;
    //    cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPos, ref velocity, GameSettings.Instance.smoothTime);
    //}
    //Cancelled because it's causing latency in lateupdate or update
    private void FixedUpdate()
    {
        targetPos = knifeTransform.position + offset;
        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPos, ref velocity, GameSettings.Instance.settings.smoothTime);
    }
    void CalculateDistance()
    {
        offset = cameraTransform.position - knifeTransform.position;
    }
}

