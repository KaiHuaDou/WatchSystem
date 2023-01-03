using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace WatchMouse
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            InsertMouseHook();
        }

        internal struct Mouse_LL_Hook_Data
        {
            internal long yx;
            internal readonly int mouseData;
            internal readonly uint flags;
            internal readonly uint time;
            internal readonly IntPtr dwExtraInfo;
        }

        private static IntPtr pMouseHook = IntPtr.Zero;
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static HookProc mouseHookProc;
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr pInstance, int threadID);
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        private int mouseHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            Mouse_LL_Hook_Data mhd = (Mouse_LL_Hook_Data)Marshal.PtrToStructure(lParam, typeof(Mouse_LL_Hook_Data));
            string state = "";
            if ((uint) wParam == 0x200)
                return 0;
            if ((uint) wParam == 513)
                state = "左键按下";
            if ((uint) wParam == 514)
                state = "左键抬起";
            if ((uint) wParam == 516)
                state = "右键按下";
            if ((uint) wParam == 517)
                state = "右键抬起";
            if ((uint) wParam == 519)
                state = "中键按下";
            if ((uint) wParam == 520)
                state = "中键抬起";
            if ((uint) wParam == 522)
                state = "滚轮滚动";
            AllKeys.Items.Add(state + " " + (mhd.yx & 0xffffffff) + "," + (mhd.yx >> 32));
            AllKeys.TopIndex = Math.Max(AllKeys.Items.Count - AllKeys.ClientSize.Height / AllKeys.ItemHeight + 1, 0);
            return 0;
        }
        private bool InsertMouseHook()
        {
            if (pMouseHook == IntPtr.Zero)
            {
                mouseHookProc = mouseHookCallback;
                pMouseHook = SetWindowsHookEx(14, mouseHookProc, (IntPtr)0, 0);
                if (pMouseHook == IntPtr.Zero)
                {
                    removeMouseHook();
                    return false;
                }
            }
            return true;
        }
        private static bool removeMouseHook()
        {
            if (pMouseHook != IntPtr.Zero)
            {
                if (UnhookWindowsHookEx(pMouseHook))
                    pMouseHook = IntPtr.Zero;
                else
                    return false;
            }
            return true;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            AllKeys.Items.Clear();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string result = "";
            foreach(string str in AllKeys.Items)
            {
                result += (str + "\n");
            }
            File.WriteAllText("default.mouse", result);
        }
    }
}