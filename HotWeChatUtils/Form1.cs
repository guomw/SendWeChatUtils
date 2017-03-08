using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotWeChatUtils
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const int WM_SETTEXT = 0x000C;
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "[商品标题]\n[分隔符]\n【原价】：[商品价格]元\n【券后】：[券后价格]元\n【口令】：[二合一淘口令]\n[分隔符]\n购买方式：复制这条信息，打开『手" +
      "机淘宝』即可看到商品和优惠券，先领券再下单哦\n[分隔符]\n本群都是内部优惠券，敬请大家关注每天特价产品。\n";
            Clipboard.SetText(str);
            IntPtr hWin = WinApi.GetWeChatWindowEx();
            //var wins = WinApi.GetAllDesktopWindows();
            //if (wins != null && wins.Count() > 0)
            //{
            //    foreach (var win in wins)
            //    {
            //        WinApi.ShowWindow(hWin, WinApi.NCmdShowFlag.SW_SHOWNORMAL);
            //        Clipboard.SetText("hello world 中文");
            //        SendKeys.SendWait("^v");
            //        //SendKeys.SendWait("{ENTER}");
            //        //WinApi.InputStr(win.hWnd, "hello world 中午");
            //    }
            //}
            if (hWin != IntPtr.Zero)
            {
                WinApi.ShowWindow(hWin, WinApi.NCmdShowFlag.SW_SHOWNORMAL);
                RECT rc = new RECT();
                WinApi.GetWindowRect(hWin, ref rc);
                int x = rc.Left; // X coordinate of the click 
                int y = rc.Top; // Y coordinate of the click 
                SetForegroundWindow(hWin);

                IntPtr lParam =(IntPtr)((y << 534) | x); // The coordinates                
                IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                const uint downCode = 0x201; // Left click down code 
                const uint upCode = 0x202; // Left click up code 
                System.Threading.Thread.Sleep(3000);
                SendMessage(hWin, downCode, wParam, lParam); // Mouse button down 
                SendMessage(hWin, upCode, wParam, lParam); // Mouse button up 

                        
                
                //WinApi.InputStr(hWin, str);
                //SendMessage(hWin, WM_SETTEXT, IntPtr.Zero, new StringBuilder("Hello World! 中文"));
                //WinApi.InputStr(hWin, "hello world 中午");
                //SendKeys.SendWait("文件传输助手");
                //回车
                //WinApi.keybd_event(Keys.Enter, 0, 0, 0);
            }
        }
    }
}
