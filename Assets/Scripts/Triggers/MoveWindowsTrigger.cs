using System;
using System.Collections;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class MoveWindowsTrigger : MonoBehaviour
{
    private IntPtr hWnd;
    public string WindowName;
    public Vector2Int Movement; // 이동할 X, Y 값 (int로 변경)
    public float duration; // 이동에 걸리는 시간
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    int previousLerpValueX; // int로 변경
    int previousLerpValueY; // int로 변경
    int targetMovementX; // int로 변경
    int targetMovementY; // int로 변경
    int initialMovementX; // int로 변경
    int initialMovementY; // int로 변경
    float elapsedTime = 0;

    void Awake()
    {
        hWnd = Win32API.FindWindow(WindowName, WindowName);
    }

    public void TriggerMovement()
    {
        Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
        initialMovementX = rect.Left;
        initialMovementY = rect.Top;
        targetMovementX = initialMovementX + Movement.x;
        targetMovementY = initialMovementY + Movement.y;
        previousLerpValueX = initialMovementX;
        previousLerpValueY = initialMovementY;
        elapsedTime = 0;
        hWnd = Win32API.FindWindow(WindowName, WindowName);
        StartCoroutine(MoveWindowCoroutine());
    }

    private IEnumerator MoveWindowCoroutine()
    {
        while (elapsedTime < duration)
        {
            Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
            elapsedTime += Time.fixedDeltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
            float easedTime = EasingFunctions.GetEasingFunction(easingType, normalizedTime);

            int currentMovementX = Mathf.RoundToInt(Mathf.Lerp(initialMovementX, targetMovementX, easedTime)); // float에서 int로 변경
            int currentMovementY = Mathf.RoundToInt(Mathf.Lerp(initialMovementY, targetMovementY, easedTime)); // float에서 int로 변경
            int deltaMovementX = currentMovementX - previousLerpValueX;
            int deltaMovementY = currentMovementY - previousLerpValueY;

            Win32API.MoveWindow(hWnd, rect.Left + deltaMovementX, rect.Top + deltaMovementY, rect.Right - rect.Left, rect.Bottom - rect.Top, true);

            previousLerpValueX = currentMovementX;
            previousLerpValueY = currentMovementY;

            Debug.Log($"{rect.Left}, {rect.Top}, {rect.Right - rect.Left}, {rect.Bottom - rect.Top}");

            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") TriggerMovement();
    }
}
