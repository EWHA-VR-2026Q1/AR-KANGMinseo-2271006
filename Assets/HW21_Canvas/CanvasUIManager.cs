using UnityEngine;
using TMPro;

public class CanvasUIManager : MonoBehaviour
{
    public TMP_Text overlayText;
    public TMP_Text cameraText;
    public TMP_Text worldText;

    public void ChangeOverlay()
    {
        overlayText.text = "Overlay Clicked!";
    }

    public void ChangeCamera()
    {
        cameraText.text = "Camera Clicked!";
    }

    public void ChangeWorld()
    {
        worldText.text = "World Clicked!";
    }
}