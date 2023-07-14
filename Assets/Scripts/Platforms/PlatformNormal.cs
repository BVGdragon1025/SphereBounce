using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNormal : Platform
{
    public override void BounceSphere()
    {
        SphereController.Instance.sphereRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }
}
