using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace WatchKeyboard
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            InsertKeyboardHook();
        }
        internal struct Keyboard_LL_Hook_Data
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr extraInfo;
        }
        private static IntPtr pKeyboardHook = IntPtr.Zero;
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static HookProc keyboardHookProc;
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr pInstance, int threadID);
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr pHookHandle);
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam); //parameter 'hhk' is ignored.
        private int keyboardHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            Keyboard_LL_Hook_Data khd = (Keyboard_LL_Hook_Data)Marshal.PtrToStructure(lParam, typeof(Keyboard_LL_Hook_Data));
            string state = khd.flags == 0 ? "按下" : (khd.flags == 128 || khd.flags == 129 ? "抬起" : "其他");
            if (char.IsLetterOrDigit((char)khd.vkCode))
                AllKeys.Items.Add(state + " " + (char)khd.vkCode);
            else AllKeys.Items.Add(state + " " +  "{" + khd.vkCode + "}");
            AllKeys.TopIndex = Math.Max(AllKeys.Items.Count - AllKeys.ClientSize.Height / AllKeys.ItemHeight + 1, 0);
            return 0;
        }
        private bool InsertKeyboardHook()
        {
            if (pKeyboardHook == IntPtr.Zero)
            {
                keyboardHookProc = keyboardHookCallback;
                pKeyboardHook = SetWindowsHookEx(13, keyboardHookProc, (IntPtr)0, 0);
                if (pKeyboardHook == IntPtr.Zero)
                {
                    removeKeyboardHook();
                    return false;
                }
            }
            return true;
        }
        private static bool removeKeyboardHook()
        {
            if (pKeyboardHook != IntPtr.Zero)
            {
                if (UnhookWindowsHookEx(pKeyboardHook))
                    pKeyboardHook = IntPtr.Zero;
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
            foreach (string str in AllKeys.Items)
            {
                result += (str + "\n");
            }
            File.WriteAllText("default.keyboard", result);
        }
    }
}