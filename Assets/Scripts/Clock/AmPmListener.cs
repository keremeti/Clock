using UnityEngine;

// реализует 24-часовой формат стрелки
public class AmPmListener : MonoBehaviour
{
    private HandsController handsController;

    private float currentRotation; 
    private float lastRotation;

    private void Awake()
    {
        handsController = GetComponent<HandsController>();
    }

    void Start()
    {
        currentRotation = transform.localEulerAngles.y;
    }

    void Update()
    {
        lastRotation = currentRotation;
        currentRotation = transform.localEulerAngles.y;
        // проверка на пересечение 12 или 00 часов
        if (handsController.state == HandsController.HandsState.AM)
        {
            if ((currentRotation > 270 & lastRotation < 90) |
                (currentRotation < 90 & lastRotation > 270))
            {
                handsController.state = HandsController.HandsState.PM;
            }
        }else if ((currentRotation > 270 & lastRotation < 90) |
            (currentRotation < 90 & lastRotation > 270))
        {
            handsController.state = HandsController.HandsState.AM;
        }
    }
}
