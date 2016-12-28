using System;
using System.Runtime.InteropServices;

namespace EightPuzzleR
{
    internal static class Interoperability
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
