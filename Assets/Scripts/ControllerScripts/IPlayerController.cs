using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController
{
    bool WantsToJump();

    bool WantsToDuck();

    bool WantsToStopDucking();
}
