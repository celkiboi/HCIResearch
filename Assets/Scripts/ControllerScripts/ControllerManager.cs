using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static IPlayerController SelectedController { get; set; }

    void Start()
    {
        PlayerBehaviour.PlayerController = SelectedController;
        GameManager.PlayerController = SelectedController;
    }
}
