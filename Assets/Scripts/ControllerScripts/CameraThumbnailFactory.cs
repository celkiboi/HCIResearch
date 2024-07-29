using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class CameraThumbnailFactory : IInputThumbnailFactory
{
    public static CameraThumbnailFactory Instance = new();

    private CameraThumbnailFactory() { }

    public Sprite RestartImage => Resources.Load<Sprite>("Input/keyboard/keyboard_r");

    public Sprite JumpImage => Resources.Load<Sprite>("Input/camera/jump-camera");

    public Sprite DuckImage => Resources.Load<Sprite>("Input/camera/duck-camera");

    public Sprite MainMenuImage => Resources.Load<Sprite>("Input/keyboard/keyboard_m");
}
