using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class RenderingTexture : MonoBehaviour
{
    public string WindowName;
    public Camera renderCamera;
    public RenderTexture renderTexture;
    private Texture2D texture2D;
    public IntPtr hWnd; // Win32 창의 핸들을 얻습니다.

    // Start는 게임 시작 시 호출되는 메서드입니다.
    void Start()
    {
        renderCamera.targetTexture = renderTexture;
        if (renderTexture == null) Debug.Log("renderTexture 없음");
        texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        if (texture2D == null) Debug.Log("texture2D 없음");
    }

    void Update()
    {
        hWnd = Win32API.FindWindow(WindowName, WindowName);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        RenderToWindow(texture2D);
    }

    // Win32 API 함수들
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr hDC);

    [DllImport("user32.dll")]
    public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi, uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

    // 구조체들
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public RGBQUAD bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth, biHeight;
        public ushort biPlanes, biBitCount;
        public uint biCompression, biSizeImage;
        public int biXPelsPerMeter, biYPelsPerMeter;
        public uint biClrUsed, biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte rgbBlue, rgbGreen, rgbRed, rgbReserved;
    }

    const int SRCCOPY = 0x00CC0020;

    private void RenderToWindow(Texture2D texture)
    {
        IntPtr hDC = GetDC(hWnd);
        IntPtr hMemDC = CreateCompatibleDC(hDC);

        BITMAPINFO bmi = new BITMAPINFO();
        bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
        bmi.bmiHeader.biWidth = texture.width;
        bmi.bmiHeader.biHeight = -texture.height; // 상하 반전 방지
        bmi.bmiHeader.biPlanes = 1;
        bmi.bmiHeader.biBitCount = 32; // 32비트 RGBA
        bmi.bmiHeader.biCompression = 0; // BI_RGB, 압축 없음

        IntPtr pBits;
        IntPtr hBitmap = CreateDIBSection(hMemDC, ref bmi, 0, out pBits, IntPtr.Zero, 0);

        IntPtr hOld = SelectObject(hMemDC, hBitmap);

        // Texture2D에서 픽셀 데이터를 가져옵니다.
        var data = texture.GetRawTextureData();
        for (int i = 0; i < data.Length; i += 4)
        {
            // RGBA to BGRA
            byte temp = data[i];
            data[i] = data[i + 2];
            data[i + 2] = temp;
        }

        // 픽셀 데이터를 비트맵에 복사합니다.
        if (pBits != IntPtr.Zero)
        {
            Marshal.Copy(data, 0, pBits, data.Length);
        }

        // 메모리 DC에서 윈도우 DC로 내용을 복사합니다.
        BitBlt(hDC, 0, 0, texture.width, texture.height, hMemDC, 0, 0, SRCCOPY);

        // 사용한 객체들을 정리합니다.
        SelectObject(hMemDC, hOld);
        DeleteObject(hBitmap);
        DeleteDC(hMemDC);
        ReleaseDC(hWnd, hDC);
    }

    void OnApplicationQuit()
    {
        Win32API.UnregisterClass(WindowName, hWnd);
        Win32API.DestroyWindow(hWnd);
    }
}
