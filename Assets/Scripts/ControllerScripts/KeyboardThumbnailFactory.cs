using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class KeyboardThumbnailFactory : IInputThumbnailFactory
{
    public static KeyboardThumbnailFactory Instance = new();

    private KeyboardThumbnailFactory() { }

    public Sprite RestartImage => Resources.Load<Sprite>("Input/keyboard/keyboard_r");

    public Sprite JumpImage => Resources.Load<Sprite>("Input/keyboard/keyboard_arrows_up");

    public Sprite DuckImage => Resources.Load<Sprite>("Input/keyboard/keyboard_arrows_down");

    public Sprite MainMenuImage => Resources.Load<Sprite>("Input/keyboard/keyboard_m");
}

