using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    readonly KeyboardPlayerController KeyboardController = new();

    void Start()
    {
        PlayerBehaviour.PlayerController = KeyboardController;
    }
}
