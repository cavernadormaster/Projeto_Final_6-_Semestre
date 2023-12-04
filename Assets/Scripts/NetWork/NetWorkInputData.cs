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
    public NetworkBool isZumbi;
    public NetworkBool started;
    public NetworkBool Walking;
    public NetworkBool HasAExit;
    public NetworkBool DOOR1;
    public NetworkBool DOOR2;
    public NetworkBool DOOR3;
    public NetworkBool DOOR4;
    public NetworkBool segurando;



}
