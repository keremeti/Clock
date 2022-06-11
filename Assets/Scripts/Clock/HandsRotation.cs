using System;
using UnityEngine;

public class HandsRotation : MonoBehaviour
{
    private float deltaAngle;
    private HandsController handsController;

    private void Awake()
    {
        handsController = GetComponent<HandsController>();
        // угол поворота
        deltaAngle = handsController.DeltaAngle;
    }

    public void Move(int angle)
    {
        transform.localRotation = Quaternion.Euler(0, angle * this.deltaAngle, 0);
    }

    // передвинуть стрелку, к области на которую нажали
    public void Move()
    {
        var position = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        double axisX = position.x;
        double axisY = position.z;
        float newAngle = transform.localEulerAngles.y;

        // выяснение четверти в координатной плоскости для верного выставления
        // угла поворота с плавным движение за целью
        if (axisY != 0 && axisX != 0)
        {
            if (axisX == 0)
            {
                if (axisY > 0)
                {
                    newAngle = 0;
                }
                else
                {
                    newAngle = 180;
                }
            }
            else if (axisX > 0)
            {
                newAngle = Convert.ToSingle(90 - Math.Atan(axisY / axisX) * (180 / Math.PI));
            }
            else
            {
                newAngle = Convert.ToSingle(270 - Math.Atan(axisY / axisX) * (180 / Math.PI));
            }

            transform.localEulerAngles = new Vector3(0, newAngle, 0);
        }
        // сохранение угла стрелки в состоянии будильника
        handsController.AlarmRotation = newAngle;
    }
}


