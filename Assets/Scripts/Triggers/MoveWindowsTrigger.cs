using System;
using System.Collections;
using UnityEngine;

public class MoveWindowsTrigger : MonoBehaviour
{
    private IntPtr hWnd;
    public string WindowName;
    public Vector2Int Movement; // 이동할 X, Y 값 (int로 변경)
    public float duration; // 이동에 걸리는 시간
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public bool SetMovement;
    public bool isTopLeftAnchor;
    int previousLerpValueX; // int로 변경
    int previousLerpValueY; // int로 변경
    int targetMovementX; // int로 변경
    int targetMovementY; // int로 변경
    int initialMovementX; // int로 변경
    int initialMovementY; // int로 변경
    float elapsedTime = 0;
    int monitorX = Win32API.GetSystemMetrics(0) / 10; // 10으로 가정
    int monitorY = Win32API.GetSystemMetrics(1) / 10; // 10으로 가정

    public void TriggerMovement()
    {
        Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
        initialMovementX = rect.Left;
        initialMovementY = rect.Top;
        if (SetMovement)
        {
            targetMovementX = Movement.x;
            targetMovementY = Movement.y;
        }
        else
        {
            targetMovementX = initialMovementX + Movement.x;
            targetMovementY = initialMovementY + Movement.y;
        }
        if (!isTopLeftAnchor)
        {
            targetMovementX = (targetMovementX * monitorX) - ((rect.Right - rect.Left) * monitorX / 2);
            targetMovementY = (targetMovementY * monitorY) - ((rect.Bottom - rect.Top) * monitorY / 2);
        }
        else
        {
            targetMovementX = (targetMovementX * monitorX);
            targetMovementY = (targetMovementY * monitorY);
        }
        previousLerpValueX = initialMovementX;
        previousLerpValueY = initialMovementY;
        elapsedTime = 0;
        StartCoroutine(MoveWindowCoroutine());
    }

    private IEnumerator MoveWindowCoroutine()
    {
        while (elapsedTime <= duration)
        {
            Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
            elapsedTime += Time.deltaTime;
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
        Win32API.GetWindowRect(hWnd, out Win32API.RECT Rect);
        Debug.Log($"{Rect.Left}, {Rect.Top}, {Rect.Right - Rect.Left}, {Rect.Bottom - Rect.Top}");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            hWnd = Win32API.FindWindow(WindowName, WindowName);
            TriggerMovement();
        }
    }
}
