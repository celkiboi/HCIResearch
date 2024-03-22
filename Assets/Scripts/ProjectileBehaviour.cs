using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(GameManager.GameSpeed * Time.deltaTime * Vector2.left);
    }
}
