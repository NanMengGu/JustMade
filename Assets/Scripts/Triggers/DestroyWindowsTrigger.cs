using System;
using UnityEngine;

public class DestroyWindowsTrigger : MonoBehaviour
{
    public string windowName;
    private IntPtr hWnd;
    void Start()
    {
        hWnd = Win32API.FindWindow(windowName, windowName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") Win32API.DestroyWindow(hWnd);
    }
}