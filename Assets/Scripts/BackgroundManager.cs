using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    GameObject background0;
    [SerializeField]
    GameObject background1;
    [SerializeField]
    GameObject ground0;
    [SerializeField]
    GameObject ground1;

    [SerializeField]
    float paralaxSpeedMultiplier = 1.4f;
    private bool hasStarted;

    Vector3 warpPoint;
    float xOffset;

    void Start()
    {
        xOffset = background1.transform.position.x - background0.transform.position.x;
        warpPoint = new
            (background0.transform.position.x - xOffset,
            background1.transform.position.y,
            background1.transform.position.z);
    }

    void FixedUpdate()
    {
        float paralaxSpeed = GameManager.GameSpeed / paralaxSpeedMultiplier;

        if (background0.transform.position.x < warpPoint.x)
        {
            Vector3 position = background0.transform.position;
            Vector3 otherPosition = background1.transform.position;
            background0.transform.position = new Vector3(otherPosition.x + xOffset, position.y, position.z);
        }
        if (background1.transform.position.x < warpPoint.x)
        {
            Vector3 position = background1.transform.position;
            Vector3 otherPosition = background0.transform.position;
            background1.transform.position = new Vector3(otherPosition.x + xOffset, position.y, position.z);
        }

        if(ground0.transform.position.x < warpPoint.x)
        {
            Vector3 position = ground0.transform.position;
            Vector3 otherPosition = ground1.transform.position;
            ground0.transform.position = new Vector3(otherPosition.x + xOffset, position.y, position.z);
        }
        if(ground1.transform.position.x < warpPoint.x)
        {
            Vector3 position = ground1.transform.position;
            Vector3 otherPosition = ground0.transform.position;
            ground1.transform.position = new Vector3(otherPosition.x + xOffset, position.y, position.z);
        }

        if (hasStarted)
        {
            background0.transform.Translate(paralaxSpeed * Time.deltaTime * Vector2.left);
            background1.transform.Translate(paralaxSpeed * Time.deltaTime * Vector2.left);
            ground0.transform.Translate(GameManager.GameSpeed * Time.deltaTime * Vector2.left);
            ground1.transform.Translate(GameManager.GameSpeed * Time.deltaTime * Vector2.left);
        }
    }

    public void StartBackgroundMovement()
    {
        hasStarted = true;
    }


}