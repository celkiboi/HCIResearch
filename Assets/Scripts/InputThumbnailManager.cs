using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputThumbnailManager : MonoBehaviour
{
    public static IInputThumbnailFactory SelectedThumbnails;

    [SerializeField]
    Image jump;
    [SerializeField]
    Image duck;
    [SerializeField]
    Image restart;
    [SerializeField]
    Image mainMenu;

    public void Start()
    {
        jump.sprite = SelectedThumbnails.JumpImage;
        duck.sprite = SelectedThumbnails.DuckImage;
        restart.sprite = SelectedThumbnails.RestartImage;
        mainMenu.sprite = SelectedThumbnails.MainMenuImage;
    }
}
