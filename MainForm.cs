using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyFlexApp
{
    public partial class MainForm : Form
    {
        // キーコード定義
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WH_KEYBOARD_LL = 13;
        private const int INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const Keys NoConvertKey = (Keys)0x1D;   // VK_NONCONVERT
        private const Keys ConvertKey = (Keys)0x1C;     // VK_CONVERT

        private readonly Dictionary<Keys, Keys> keyMappings = new Dictionary<Keys, Keys>
        {
            { NoConvertKey, Keys.LShiftKey },  // 無変換 → 左Shift
            { ConvertKey, Keys.RShiftKey }     // 変換 → 右Shift
        };

        // フック用デリゲート
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private readonly LowLevelKeyboardProc _proc;
        private readonly IntPtr _hookID = IntPtr.Zero;

        public MainForm()
        {
            InitializeComponent();
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosing(e);
        }

        private void BtnLogClear_Click(object sender, EventArgs e) => outputLog.Clear();

        private void CheckOutputLog_CheckedChanged(object sender, EventArgs e)
        {
            outputLog.Enabled = checkOutputLog.Checked;
            btnLogClear.Enabled = checkOutputLog.Checked;
        }

        // キーフック設定
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // キーイベント処理
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys pressedKey = (Keys)vkCode;

                if (keyMappings.ContainsKey(pressedKey))
                {
                    if (wParam == (IntPtr)WM_KEYDOWN)
                    {
                        SendShiftDown(keyMappings[pressedKey]);
                        AppendText($"{pressedKey} → Shift Down");
                        return (IntPtr)1; // 元のキーをブロック
                    }
                    else if (wParam == (IntPtr)WM_KEYUP)
                    {
                        SendShiftUp(keyMappings[pressedKey]);
                        AppendText($"{pressedKey} → Shift Up");
                        return (IntPtr)1;
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // TextBoxにタイムスタンプ付きで追記
        private void AppendText(string text)
        {
            if (checkOutputLog.Checked == false) return;

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string line = $"[{timestamp}] {text}";

            if (outputLog.InvokeRequired)
                outputLog.Invoke(new Action(() => outputLog.AppendText(line + Environment.NewLine)));
            else
                outputLog.AppendText(line + Environment.NewLine);
        }

        // Shiftキー送信（押下→離す）
        private void SendShiftDown(Keys shiftKey)
        {
            ushort vk = (ushort)shiftKey;
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT { wVk = vk, dwFlags = 0 }
                    }
                }
            };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        private void SendShiftUp(Keys shiftKey)
        {
            ushort vk = (ushort)shiftKey;
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT { wVk = vk, dwFlags = KEYEVENTF_KEYUP }
                    }
                }
            };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        // WinAPI 宣言
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)] public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
