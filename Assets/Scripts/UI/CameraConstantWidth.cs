using UnityEngine;

// изменение угла обзора камеры в зависимости от разрешения
public class CameraConstantWidth : MonoBehaviour
{
    private Vector2 defaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    private Camera componentCamera;
    private float ratio;

    private float startedView;
    private float horizontalView;

    private void Awake()
    {
        EventManager.OnChangeOrientation.AddListener(ChangeView);
    }

    private void Start()
    {
        componentCamera = GetComponent<Camera>();

        ratio = defaultResolution.x / defaultResolution.y;

        startedView = componentCamera.fieldOfView;
        horizontalView = CalcVerticalView(startedView, 1 / ratio);
        ChangeView();
    }

    private float CalcVerticalView(float view, float aspectRatio)
    {
        float horizontalViewInRads = view * Mathf.Deg2Rad;

        float verticalViewInRads = Mathf.Atan(Mathf.Tan(horizontalViewInRads / 2)
            / aspectRatio) * 2;

        return verticalViewInRads * Mathf.Rad2Deg;
    }

    private void ChangeView()
    {
        float constantWidthFov = CalcVerticalView(horizontalView,
            componentCamera.aspect);
        componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, startedView,
            WidthOrHeight);
    }
}