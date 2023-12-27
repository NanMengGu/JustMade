using System;
using UnityEngine;

public class StartWindowPosition : MonoBehaviour
{

    public Vector2 windowSize = new Vector2(500, 500);
    public bool centerOnScreen = true; // 화면 중앙에 위치시킬지 여부

    void Start()
    {
        Application.runInBackground = true;
        Screen.SetResolution((int)windowSize.x, (int)windowSize.y, false);

        if (centerOnScreen)
        {
            IntPtr hWnd = Win32API.GetActiveWindow();
            Resolution currentResolution = Screen.currentResolution;
            int newX = (currentResolution.width - (int)windowSize.x) / 2;
            int newY = (currentResolution.height - (int)windowSize.y) / 2;
            Win32API.SetWindowPos(hWnd, 0, newX, newY, (int)windowSize.x, (int)windowSize.y, 0x0040);
        }
    }
}
