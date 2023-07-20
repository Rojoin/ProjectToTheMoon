using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWeb : MonoBehaviour
{
#if UNITY_WEBGL
    void Start()
    {
        Destroy(gameObject);
    }
#endif
}