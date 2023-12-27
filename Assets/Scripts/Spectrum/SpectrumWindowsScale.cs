using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class SpectrumWindowsScale : MonoBehaviour
{
    private IntPtr windowHandle;
    public string WindowName;
    public AudioSource audioSource;
    public float size;
    public int sampleCount;
    private float[] sample = new float[64];
    private Win32API.RECT rect;
    private int centerX;
    private int centerY;

    void Start()
    {
        windowHandle = Win32API.FindWindow(WindowName, WindowName);
        if (windowHandle == IntPtr.Zero)
        {
            LogWin32Error();
        }
    }

    private void LogWin32Error()
    {
        int errorCode = Marshal.GetLastWin32Error();
        string errorMessage = new System.ComponentModel.Win32Exception(errorCode).Message;
        Debug.LogError($"Win32 Error Code: {errorCode}, Message: {errorMessage}");
    }

    void Update()
    {
        windowHandle = Win32API.FindWindow(WindowName, WindowName);
        if (windowHandle == IntPtr.Zero)
        {
            LogWin32Error();
        }
        Win32API.GetWindowRect(windowHandle, out rect);

        audioSource.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
        
        int newWidth = Mathf.RoundToInt(sample[sampleCount] * size);
        int newHeight = Mathf.RoundToInt(sample[sampleCount] * size);

        Win32API.MoveWindow(windowHandle, rect.Left, rect.Top, 150, newHeight, true);
    }
}
