using UnityEngine;
using UnityEngine.UI;

public class IgnoreMask : MonoBehaviour
{
    public Image targetImage; 
    public bool isRaycastTarget = true; 

    void Start()
    {
        targetImage.raycastTarget = isRaycastTarget;
    }

    public void ToggleRaycastTargetState()
    {
        isRaycastTarget = !isRaycastTarget;
        targetImage.raycastTarget = isRaycastTarget;
    }
}