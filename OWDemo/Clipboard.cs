using System;
using System.Runtime.InteropServices;

namespace OWDemo
{
    /// <summary>
    /// WinAPI access to the clipboard for easier usage.
    /// </summary>
    static class Clipboard
    {
        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);
        
        /// <summary>
        /// Sets the clipboard to the specified text.
        /// </summary>
        /// <param name="text">The text to copy to the clipboard.</param>
        public static void SetText(string text)
        {
            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(text);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
        }
    }
}
