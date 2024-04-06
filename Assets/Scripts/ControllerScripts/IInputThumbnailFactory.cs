using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInputThumbnailFactory 
{
    Sprite RestartImage { get; }
    Sprite JumpImage { get; }
    Sprite DuckImage { get; }
    Sprite MainMenuImage { get; }

}
