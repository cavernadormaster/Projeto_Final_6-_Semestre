using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetWorkInputData : INetworkInput
{
    public Vector2 movementInput;
    public float rotationInput;
    public NetworkBool isJumpPressed;
    public NetworkBool isTakeInputPressed;
    public NetworkBool isFireButtonPressed;
    public NetworkBool isCientistBlue;
    public NetworkBool isCientistRed;
    public NetworkBool isCientistYellow;
    public NetworkBool isCientistGreen;
    public NetworkBool isZumbi;
    public NetworkBool started;

}
