using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ShowClassName : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
    
    public Text text;
    // Start is called before the first frame update
    void Update()
    {
        IntPtr activeWindow = Win32API.GetActiveWindow();
        StringBuilder className = new StringBuilder(100);

        if (GetClassName(activeWindow, className, className.Capacity) > 0)
        {
            /* Debug.Log($"선택된 창의 클래스 명: {className}"); */
        }
        /* else Debug.Log("없음"); */

        string classNameString = className.ToString();
        text.text = classNameString;
    }
}
