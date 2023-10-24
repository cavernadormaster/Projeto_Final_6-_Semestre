using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetWorkInputData : INetworkInput
{
    public Vector2 movementInput;
    public float rotationInput;
    public NetworkBool isJumpPressed;

}
