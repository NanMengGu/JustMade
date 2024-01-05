using System.Collections;
using UnityEngine;
using System;

public class ScaleWindowsTrigger : MonoBehaviour
{
    private IntPtr hWnd;
    public string WindowName;
    public Vector2Int Size;
    public float duration;        // 시간을 초 단위로 변경
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public bool SetSize;
    public bool isTopLeftAnchor;
    int previousLerpValueX; // int로 변경
    int previousLerpValueY; // int로 변경
    int targetSizeX; // int로 변경
    int targetSizeY; // int로 변경
    int initialSizeX; // int로 변경
    int initialSizeY; // int로 변경
    float elapsedTime = 0;
    int monitorX = Win32API.GetSystemMetrics(0) / 10; // 10으로 가정
    int monitorY = Win32API.GetSystemMetrics(1) / 10; // 10으로 가정

    void Awake()
    {
        hWnd = Win32API.FindWindow(WindowName, WindowName);
    }

    public void TriggerMovement()
    {
        if (hWnd == IntPtr.Zero) hWnd = Win32API.FindWindow(WindowName, WindowName);
        Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
        initialSizeX = rect.Left;
        initialSizeY = rect.Top;
        if (SetSize)
        {
            targetSizeX = Size.x;
            targetSizeY = Size.y;
        }
        else
        {
            targetSizeX = initialSizeX + Size.x;
            targetSizeY = initialSizeY + Size.y;
        }
        targetSizeX = (targetSizeX * monitorX);
        targetSizeY = (targetSizeY * monitorY);
        previousLerpValueX = initialSizeX;
        previousLerpValueY = initialSizeY;
        elapsedTime = 0;
        hWnd = Win32API.FindWindow(WindowName, WindowName);
        StartCoroutine(ScaleWindowCoroutine());
    }

    private IEnumerator ScaleWindowCoroutine()
    {
        while (elapsedTime <= duration)
        {
            Win32API.GetWindowRect(hWnd, out Win32API.RECT rect);
            elapsedTime += Time.fixedDeltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
            float easedTime = EasingFunctions.GetEasingFunction(easingType, normalizedTime);

            int currentSizeX = Mathf.RoundToInt(Mathf.Lerp(initialSizeX, targetSizeX, easedTime)); // float에서 int로 변경
            int currentSizeY = Mathf.RoundToInt(Mathf.Lerp(initialSizeY, targetSizeY, easedTime)); // float에서 int로 변경
            int deltaSizeX = currentSizeX - previousLerpValueX;
            int deltaSizeY = currentSizeY - previousLerpValueY;

            if (isTopLeftAnchor) 
            {
                Win32API.MoveWindow(hWnd, rect.Left, rect.Top, rect.Right + deltaSizeX - rect.Left, rect.Bottom + deltaSizeY - rect.Top,true);
            }
            else
            {
                int newLeft = rect.Left - deltaSizeX / 2;
                int newTop = rect.Top - deltaSizeY / 2;
                Win32API.MoveWindow(hWnd, newLeft, newTop, rect.Right - rect.Left + deltaSizeX, rect.Bottom - rect.Top + deltaSizeY, true);
            }

            previousLerpValueX = currentSizeX;
            previousLerpValueY = currentSizeY;

            Debug.Log($"{rect.Left}, {rect.Top}, {rect.Right - rect.Left}, {rect.Bottom - rect.Top}");

            yield return null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") TriggerMovement();
    }
}
