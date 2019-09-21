using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public Texture2D crosshairImage;

    void OnGUI()
    {

        float adjustedReticleWidth = crosshairImage.width / 20;
        float adjustedReticleHeight = crosshairImage.height / 20;

        float xMin = (Screen.width / 2) - (adjustedReticleWidth / 2);
        float yMin = (Screen.height / 2) - (adjustedReticleHeight / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, adjustedReticleWidth, adjustedReticleHeight), crosshairImage);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
