using System;
using System.Runtime.InteropServices;
using UnityEngine;

/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To make all pure black rgb(0,0,0) pixels on screen invisible and click through in build
*/

public class TransparentScreen : MonoBehaviour
{
    // Import functions from user32.dll to interact with Windows-level APIs

    // Gets the handle of the currently active window
    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();

    // Sets window styles (used to add transparency)
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    // Sets the window position and Z-order (used to set window as topmost)
    [DllImport("user32.dll")]
    public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    // Sets the transparency attributes of the window
    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    // Struct required for the DWM function to define the glass margin
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    // Enables extending the window's glass effect (used for full transparency)
    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    // Constant to specify we're modifying the extended window style
    const int GWL_EXSTYLE = -20;

    // Style flags for layered and transparent windows
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    // Makes the window always stay on top
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    // Transparency option: treat a color as fully transparent
    const uint LWA_COLORKEY = 0x00000001;

    private void Start()
    {
        // This code only runs in build (not in the Unity Editor)
#if !UNITY_EDITOR
        // Get the handle to the active Unity window
        IntPtr hWnd = GetActiveWindow();

        // Create margins with -1 to extend the glass effect to the whole window
        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins); // Apply the margins

        // Set the window style to "layered" so we can modify its transparency
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);

        // Make the window fully transparent using a color key (color 0 = black)
        SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        // Set the window to be always on top
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#endif
        // Ensures the app keeps running even when it's not in focus
        Application.runInBackground = true;
    }
}