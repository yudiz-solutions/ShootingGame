using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
