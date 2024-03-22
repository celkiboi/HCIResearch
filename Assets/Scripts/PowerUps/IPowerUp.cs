using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp 
{
    void Activate();
    GameObject Player { get; set;  }
    string Text { get; }
}
