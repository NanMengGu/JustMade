using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class SpectrumWindowsScale : MonoBehaviour
{
    private IntPtr hWnd;
    public string WindowName;
    public AudioSource audioSource;
    public float size;
    public int sampleCount;
    private float[] sample = new float[64];
    private Win32API.RECT rect;

    void Update()
    {
        if (hWnd == IntPtr.Zero) hWnd = Win32API.FindWindow(WindowName, WindowName);
        Win32API.GetWindowRect(hWnd, out rect);

        audioSource.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
        
        int newWidth = Mathf.RoundToInt(sample[sampleCount] * size);
        int newHeight = Mathf.RoundToInt(sample[sampleCount] * size);

        Win32API.MoveWindow(hWnd, rect.Left, rect.Top, 150, newHeight, true);
    }
}
