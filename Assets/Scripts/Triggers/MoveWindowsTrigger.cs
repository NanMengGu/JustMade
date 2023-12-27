using System;
using System.Collections;
using UnityEngine;

public class MoveWindowsTrigger : MonoBehaviour
{
    private IntPtr windowHandle;
    public int x;
    public int y;
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public float time; // 시간을 초 단위로 변경

    void Awake()
    {
        windowHandle = Win32API.GetActiveWindow();
        if (windowHandle == IntPtr.Zero)
        {
            Debug.LogError("Active Window를 찾을 수 없습니다.");
        }
    }

    public void TriggerMovement()
    {
        StartCoroutine(MoveWindowCoroutine());
    }

    private IEnumerator MoveWindowCoroutine()
    {
        Win32API.GetWindowRect(windowHandle, out Win32API.RECT rect);

        int startX = rect.Left;
        int startY = rect.Top;
        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;
            t = EasingFunctions.GetEasingFunction(easingType, t);
            Win32API.MoveWindow(windowHandle, Mathf.RoundToInt(startX + x * t), Mathf.RoundToInt(startY + y * t), width, height, true);
            yield return null;
        }
        Win32API.MoveWindow(windowHandle, startX + x, startY + y, width, height, true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") TriggerMovement();
    }
}
