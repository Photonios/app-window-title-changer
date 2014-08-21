using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace app_window_title_changer
{
    public class Window
    {
        //http://msdn.microsoft.com/en-us/library/windows/desktop/ms633546(v=vs.85).aspx
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetWindowText(IntPtr hwnd, String lpString);

        public Process Process { get; private set; }

        public IntPtr MainWindowHandle
        {
            get
            {
                return this.Process.MainWindowHandle;
            }
        }

        public string MainWindowTitle
        {
            get
            {
                if(this.Process.MainWindowHandle == IntPtr.Zero)
                    return null;

                return this.Process.MainWindowTitle;
            }
        }

        public Window(Process proc)
        {
            this.Process = proc;
        }

        public bool SetTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
                return false;

            if(this.MainWindowHandle == IntPtr.Zero)
                return false;

            bool result = Window.SetWindowText(this.MainWindowHandle, title);
            return result;
        }
    }
}
