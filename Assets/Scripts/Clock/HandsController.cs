using System;
using UnityEngine;

// управление для стрелки
public class HandsController : MonoBehaviour
{
    public enum HandsState
    {
        AM,
        PM,
    }

    [SerializeField] private float deltaAngle; // градус поворота
    private float alarmRotation = 0; // угол стрелки в состоянии будильника
    public float DeltaAngle { get => deltaAngle; }
    private AudioSource audioSource;
    private float lastHandRotation;
    [HideInInspector] public HandsState state = HandsState.AM;

    // перевод поворота стрелки в формат времени
    public float AlarmTime
    {
        get
        {
            if (state == HandsState.AM)
            {
                return (float)Math.Truncate(alarmRotation / deltaAngle);
            }
            else
            {
                return (float)Math.Truncate((alarmRotation + 360) / deltaAngle);
            }

        }
    }

    public float AlarmRotation
    {
        set => alarmRotation = value;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        lastHandRotation =
            (float)Math.Truncate(transform.localEulerAngles.y / deltaAngle);
    }

    private void LateUpdate()
    {
        // тиканье по делениям циферблата
        if (!audioSource.isPlaying &&
            lastHandRotation !=
            (float)Math.Truncate(transform.localEulerAngles.y / deltaAngle))
        {
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.Play();
            lastHandRotation =
                (float)Math.Truncate(transform.localEulerAngles.y / deltaAngle); ;
        }
    }
}
