using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class QuitAllWindows : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool DestroyWindow(IntPtr hWnd);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    void OnApplicationQuit()
    {
        EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);
    }

    private bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
    {
        if (IsWindowVisible(hWnd))
        {
            System.Text.StringBuilder title = new System.Text.StringBuilder(256);
            GetWindowText(hWnd, title, 256);
            string windowTitle = title.ToString();

            // 유니티 에디터 창을 제외하고 창을 닫음
            if (!string.IsNullOrEmpty(windowTitle) && !windowTitle.Contains("Unity Editor"))
            {
                DestroyWindow(hWnd);
            }
        }
        return true;
    }
}
