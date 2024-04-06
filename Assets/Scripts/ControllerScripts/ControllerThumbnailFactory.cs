using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class ControllerThumbnailFactory : IInputThumbnailFactory
{
    public static ControllerThumbnailFactory Instance = new();

    private ControllerThumbnailFactory() { }

    public Sprite RestartImage => Resources.Load<Sprite>("Input/controller/xbox_button_color_a");

    public Sprite JumpImage => Resources.Load<Sprite>("Input/controller/xbox_dpad_round_up");

    public Sprite DuckImage => Resources.Load<Sprite>("Input/controller/xbox_dpad_round_down");

    public Sprite MainMenuImage => Resources.Load<Sprite>("Input/controller/xbox_button_color_b");
}

