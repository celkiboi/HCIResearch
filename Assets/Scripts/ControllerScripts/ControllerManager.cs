using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    readonly KeyboardPlayerController KeyboardController = new();
    readonly XboxPlayerController XboxController = new();

    void Start()
    {
        //PlayerBehaviour.PlayerController = KeyboardController;
        PlayerBehaviour.PlayerController = XboxController;
        GameManager.PlayerController = XboxController;
    }
}
