using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class CreateWindowTrigger : MonoBehaviour
{
    public int x;
    public int y;
    public int nWidth;
    public int nHeight;
    public string WindowName;
    public IntPtr hWnd;
    public delegate IntPtr DefWindowProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    private Camera newCamera;
    private RenderTexture newRenderTexture;
    private RenderingTexture renderingTexture;

    /* void Awake()
    {
        CreateWindow();
        CreateCamera();
    } */


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            CreateWindow();
            CreateCamera();
        }
    }

    public void CreateCamera()
    {
        // 새 카메라 생성
        GameObject cameraObject = new GameObject("NewCamera");
        cameraObject.transform.position = new Vector2(x, y);
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Cube);
        capsule.transform.position = new Vector2(x, y);
        newCamera = cameraObject.AddComponent<Camera>();
        renderingTexture = cameraObject.AddComponent<RenderingTexture>();
        newRenderTexture = new RenderTexture(nWidth - 5, nHeight - 5, 24);

        renderingTexture.WindowName = WindowName;
        renderingTexture.renderCamera = newCamera;
        renderingTexture.renderTexture = newRenderTexture;
        renderingTexture.hWnd = hWnd;
    }

    public void CreateWindow()
    {
        DefWindowProcDelegate defWndProcDel = new DefWindowProcDelegate(Win32API.DefWindowProc);
        RegisterWindowClass(WindowName, Marshal.GetFunctionPointerForDelegate(defWndProcDel));
        int dwExStyle = 0; // 확장 스타일 기본값
        int dwStyle = Win32API.WS_OVERLAPPEDWINDOW; // 스타일 기본값
        IntPtr hWndParent = IntPtr.Zero; // 부모 윈도우 핸들 기본값
        IntPtr hMenu = IntPtr.Zero; // 메뉴 핸들 기본값
        IntPtr hInstance = IntPtr.Zero; // 인스턴스 핸들 기본값
        IntPtr lpParam = IntPtr.Zero; // 추가 매개변수 기본값

        hWnd = Win32API.CreateWindowEx(
            dwExStyle,
            WindowName,
            WindowName,
            dwStyle,
            x,
            y,
            nWidth,
            nHeight,
            hWndParent,
            hMenu,
            hInstance,
            lpParam);
        if (hWnd == IntPtr.Zero)
        {
            LogWin32Error();
        }
        /* else
        {
            Debug.Log($"만들어짐 {hWnd}");
        } */
    }

    private void RegisterWindowClass(string className, IntPtr wndProc)
    {
        Win32API.WNDCLASS wndClass = new Win32API.WNDCLASS();
        wndClass.style = 0;
        wndClass.lpfnWndProc = wndProc; // 윈도우 프로시저 포인터
        wndClass.cbClsExtra = 0;
        wndClass.cbWndExtra = 0;
        wndClass.hInstance = IntPtr.Zero; // 현재 인스턴스 핸들
        wndClass.hIcon = IntPtr.Zero; // 아이콘 핸들
        wndClass.hCursor = IntPtr.Zero; // 커서 핸들
        wndClass.hbrBackground = IntPtr.Zero; // 배경 브러쉬 핸들
        wndClass.lpszMenuName = null; // 메뉴 이름
        wndClass.lpszClassName = className; // 클래스 이름

        if (Win32API.RegisterClass(ref wndClass) == 0)
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
}
