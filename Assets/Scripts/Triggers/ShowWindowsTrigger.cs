using UnityEngine;
using System;
using Unity.VisualScripting;

public class ShowWindowsTrigger : MonoBehaviour
{
    public string WindowName;
    private IntPtr hWnd;

   void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            hWnd = Win32API.FindWindow(WindowName, WindowName);
            if (hWnd == null)
            {
                Debug.Log($"hWnd 없음 {hWnd}");
                return;
            }
            Win32API.ShowWindow(hWnd, Win32API.SW_SHOW);
        }
    }
}