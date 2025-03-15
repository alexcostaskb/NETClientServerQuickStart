using System;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// A simple NetworkBehaviour that moves the object in a circle
/// </summary>
public class NetworkTransformTest : NetworkBehaviour
{
    private void Update()
    {
        if (IsServer)
        {
            // Move the object in a circle

            // Calculate the angle
            float theta = Time.frameCount / 15.0f;

            // Set the position of the object
            transform.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
        }
    }
}