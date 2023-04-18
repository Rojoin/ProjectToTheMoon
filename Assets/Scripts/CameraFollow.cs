using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    [Header("Offset")]
    public Vector3 offset = Vector3.zero;
    [Header("Limits")]
    public Vector2 limits = new Vector2(5, 3);

    [Range(0, 1)]
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (!Application.isPlaying)
        {
            transform.localPosition = offset;
        }

        FollowTarget(target);
    }

    void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    public void FollowTarget(Transform target)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = target.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), ref velocity, smoothTime);
    }



}
