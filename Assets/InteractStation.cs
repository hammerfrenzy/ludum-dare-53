using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractStation 
{
    bool RetainControlOnSwap { get; }
    public void SetInteracting(bool interacting, RatControllerBehavior rat);
}
