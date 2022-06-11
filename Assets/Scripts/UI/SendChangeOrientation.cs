using UnityEngine;

// отслеживание поворота устройства
public class SendChangeOrientation : MonoBehaviour
{
    void OnRectTransformDimensionsChange()
    {
        EventManager.SendChangeOrientation();
    }
}