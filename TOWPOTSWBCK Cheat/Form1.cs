using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace TOWPOTSWBCK_Cheat
{
    public partial class Form1 : Form
    {
        // Import
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool GetAsyncKeyState(int VirtualKeyPressed);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        // Vars
        public bool toggled;
        public IntPtr windowHandle;
        public IntPtr window = GetForegroundWindow();
        public IntPtr handle = FindWindowA(null, "The one who pulls out the sword will be crowned king");
        Thread thread;

        public Form1()
        {
            InitializeComponent();

            // Update toggled status when form opens
            if (toggled)
            {
                toggleStatus.Text = "toggled";
                toggleStatus.ForeColor = Color.Green;
            }

            else
            {
                toggleStatus.Text = "untoggled";
                toggleStatus.ForeColor = Color.Red;
            }

            // Automatic configuration
            cheatDelay.Text = "1000";
            cheatSpeed.Text = "100";

            // Font installing
            AddFontResource(@"C:\Users\Nico\source\repos\TOWPOTSWBCK Cheat\TOWPOTSWBCK Cheat\ProggyClean.ttf");

        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return null;
        }

        // Change toggled status label and color
        private void button1_Click(object sender, EventArgs e)
        {
            toggled = !toggled;

            cheatMain();
        }

        public void textColor(int textNumber)
        {
            // Bracket coloring
            string c1 = "["; string c2 = "]";
            int index = output.Text.IndexOf("["); int index2 = output.Text.IndexOf("]");
            int lenght = c1.Length; int lenght2 = c2.Length;
            output.Select(index, lenght);
            output.SelectionColor = Color.White;
            output.Select(index2, lenght2);
            output.SelectionColor = Color.White;


            // Text coloring
            // Vars
            string text;
            int indexText;
            int lenghtText;

            switch (textNumber)
            {
                case 1:
                    text = "didn't found game window";
                    indexText = output.Text.IndexOf("didn't found game window");
                    lenghtText = text.Length;
                    output.Select(indexText, lenghtText);
                    output.SelectionColor = Color.White;
                    break;

                case 2:
                    text = "game window found";
                    indexText = output.Text.IndexOf("game window found");
                    lenghtText = text.Length;
                    output.Select(indexText, lenghtText);
                    output.SelectionColor = Color.White;
                    break;
            }
        }

        public void cheatMain()
        {
            if (toggled)
            {
                toggled = true;
                toggleStatus.Text = "toggled";
                toggleStatus.ForeColor = Color.Green;

                // Didn't found game window
                if (handle == IntPtr.Zero)
                {
                    output.Text += "[ERROR] didn't found game window" + Environment.NewLine;
                    textColor(1);

                    toggled = false;
                    toggleStatus.Text = "untoggled";
                    toggleStatus.ForeColor = Color.Red;
                    return;
                }

                // Found game window
                else
                {
                    output.Text += "[SwordWare] game window found" + Environment.NewLine;
                    textColor(2);

                    // We're gonna use a different thread, so the GUI doesn't crash when we do a loop
                    thread = new Thread(t =>
                    {
                        // Parsing config (loop for changing it on real time)
                        int speed = int.Parse(cheatSpeed.Text);
                        int delay = int.Parse(cheatDelay.Text);

                        // Thread config
                        thread.IsBackground = true;

                        bool son = false;

                        // Loop function
                        for (int i = 0; i >= 0; i++)
                        {
                            // If user is clicking
                            if (GetAsyncKeyState(0x01) && toggled)
                            {
                                // Only on window of the game
                                if (GetActiveWindowTitle() == "The one who pulls out the sword will be crowned king")
                                {
                                    // For waiting the delay time just one time
                                    if (!son)
                                    {
                                        Thread.Sleep(delay); // If there isn't a delay before the mouse starts moving, the user won0't grab the sword
                                        son = true;
                                    }
                                    
                                    mouse_event(0x0001, 0, speed * -1, 0, 0); // Speed is negative (* -1) because we want the mouse to go up
                                }
                            }

                            // Restart the bool
                            if (!GetAsyncKeyState(0x01))
                            {
                                son = false;
                            }
                        }
                    });

                    thread.Start();
                }
            }

            else
            {
                toggled = false;
                toggleStatus.Text = "untoggled";
                toggleStatus.ForeColor = Color.Red;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Move GUI
        int m, mx, my;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1;
            mx = e.X;
            my = e.Y;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cheatSpeed_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cheatSpeed_Validated(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(cheatSpeed.Text, "[^0-9]"))
            {
                cheatSpeed.Text = new string(cheatSpeed.Text.Where(c => char.IsDigit(c)).ToArray());
            }

            if (cheatSpeed.Text == String.Empty)
            {
                cheatSpeed.Text = "100";
            }

            else
            {
                int speed = int.Parse(cheatSpeed.Text);

                if (speed > 5000)
                {
                    cheatSpeed.Text = "5000";
                }

                if (speed <= 100 || cheatSpeed.Text == null)
                {
                    cheatSpeed.Text = "100";
                }
            }

        }

        private void cheatDelay_Validated(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(cheatDelay.Text, "[^0-9]"))
            {
                cheatDelay.Text = new string(cheatDelay.Text.Where(c => char.IsDigit(c)).ToArray());
            }

            if (cheatDelay.Text == String.Empty)
            {
                cheatDelay.Text = "200";
            }

            else
            {
                int delay = int.Parse(cheatDelay.Text);

                if (delay > 1000)
                {
                    cheatDelay.Text = "1000";
                }

                if (delay <= 200 || cheatDelay.Text == null)
                {
                    cheatDelay.Text = "200";
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}