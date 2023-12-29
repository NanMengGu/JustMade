using System.Collections;
using UnityEngine;
using System;

public class ScaleWindowsTrigger : MonoBehaviour
{
    private IntPtr hWnd;
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public string WindowName;
    public int targetWidth;   // 목표 너비
    public int targetHeight;  // 목표 높이
    public float time;        // 시간을 초 단위로 변경

    private Win32API.RECT currentRect;  // 현재 윈도우 상태를 저장하는 변수

    void Awake()
    {
        hWnd = Win32API.FindWindow(WindowName, WindowName);
        UpdateCurrentRect();  // 현재 윈도우 상태 업데이트
    }

    private void UpdateCurrentRect()
    {
        if (Win32API.GetWindowRect(hWnd, out Win32API.RECT rect))
        {
            currentRect = rect;
        }
    }

    public void TriggerMovement()
    {
        if (hWnd == IntPtr.Zero) hWnd = Win32API.FindWindow(WindowName, WindowName);
        StartCoroutine(ScaleWindowCoroutine());
    }

    private IEnumerator ScaleWindowCoroutine()
    {
        UpdateCurrentRect();  // 트리거 실행 전 현재 상태 업데이트

        int startX = currentRect.Left;
        int startY = currentRect.Top;
        int startWidth = currentRect.Right - currentRect.Left;
        int startHeight = currentRect.Bottom - currentRect.Top;

        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;
            t = EasingFunctions.GetEasingFunction(easingType, t);

            int newWidth = Mathf.RoundToInt(startWidth + (targetWidth - startWidth) * t);
            int newHeight = Mathf.RoundToInt(startHeight + (targetHeight - startHeight) * t);

            // 새로운 창 위치 계산
            int newX = startX - (newWidth - startWidth) / 2;
            int newY = startY - (newHeight - startHeight) / 2;

            Win32API.MoveWindow(hWnd, newX, newY, newWidth, newHeight, true);
            yield return null;
        }

        // 최종 크기와 위치 설정
        int finalX = startX - (targetWidth - startWidth) / 2;
        int finalY = startY - (targetHeight - startHeight) / 2;
        Win32API.MoveWindow(hWnd, finalX, finalY, startWidth + targetWidth, startHeight + targetHeight, true);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") TriggerMovement();
    }
}
