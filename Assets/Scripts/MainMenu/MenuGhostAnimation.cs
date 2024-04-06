using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGhostAnimation : MonoBehaviour
{
    [SerializeField]
    Image ghostImage;

    [SerializeField]
    Sprite normal;
    [SerializeField]
    Sprite normalSmile;
    [SerializeField]
    Sprite duck;
    [SerializeField]
    Sprite duckSmile;

    [SerializeField]
    int changeStanceTicks = 100;
    [SerializeField]
    int changeSmileTicks = 50;
    [SerializeField]
    int smileDelayTicks = 100;

    int currentStanceTicks = 0;
    int currentSmileTicks = 0;
    int currentSmileDelayTicks = 0;

    bool isSmiling = false;
    bool isDucking = false;

    private void Start()
    {
        ghostImage.sprite = normal;
        ghostImage.SetNativeSize();
    }

    void FixedUpdate()
    {
        currentSmileDelayTicks++;
        currentStanceTicks++ ;
        if (currentSmileDelayTicks > smileDelayTicks)
            currentSmileTicks++;

        if (currentStanceTicks >= changeStanceTicks ) 
        {
            if (isDucking)
            {
                isDucking = false;
                ghostImage.sprite = normal;
                ghostImage.SetNativeSize();
            }
            else 
            {
                isDucking = true;
                ghostImage.sprite = duck;
                ghostImage.SetNativeSize();
            }

            currentStanceTicks = 0;
        }

        if (currentSmileTicks >= changeSmileTicks ) 
        {
            if (isSmiling) 
            {
                isSmiling = false;
                ghostImage.sprite =  isDucking ? duck : normal;
                ghostImage.SetNativeSize();
                currentSmileDelayTicks = 0;
            }
            else
            {
                isSmiling = true;
                ghostImage.sprite = isDucking ? duckSmile : normalSmile;
                ghostImage.SetNativeSize();
            }

            currentSmileTicks = 0;
        }
    }
}
