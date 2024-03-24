using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp 
{
    void Activate();
    string Text { get; }
    GameObject Player { get; set; }
    HUDUpdater HUDUpdater { get; set; }
    PowerupSpawner PowerupSpawner { get; set; }
    GameManager GameManagerInstance { get; set; }
}
