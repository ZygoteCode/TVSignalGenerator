using MetroSuite;
using ProtoRandom;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

public partial class MainForm : MetroForm
{
    private Thread currentGenerationThread;

    public MainForm()
    {
        InitializeComponent();
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
    }

    private void gunaButton1_Click(object sender, System.EventArgs e)
    {
        if (gunaButton1.Text.Equals("Start generating your TV signal"))
        {
            gunaButton1.Text = "Stop generating your TV signal";
            currentGenerationThread = new Thread(GenerateTVSignal);
            currentGenerationThread.Priority = ThreadPriority.Highest;
            currentGenerationThread.Start();
        }
        else
        {
            currentGenerationThread.Abort();
            gunaButton1.Text = "Start generating your TV signal";
            pictureBox1.BackgroundImage = null;
        }
    }

    public void GenerateTVSignal()
    {
        while (true)
        {
            ProtoRandom.ProtoRandom random = new ProtoRandom.ProtoRandom(1);
            Bitmap bitmap = new Bitmap(128, 128);

            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    int generated = random.GetRandomInt32(0, 4);

                    if (generated == 0)
                    {
                        bitmap.SetPixel(i, j, Color.White);
                    }
                    else if (generated == 1)
                    {
                        bitmap.SetPixel(i, j, Color.LightGray);
                    }
                    else if (generated == 2)
                    {
                        bitmap.SetPixel(i, j, Color.Gray);
                    }
                    else if (generated == 3)
                    {
                        bitmap.SetPixel(i, j, Color.DarkGray);
                    }
                    else if (generated == 4)
                    {
                        bitmap.SetPixel(i, j, Color.Black);
                    }
                }
            }

            pictureBox1.BackgroundImage = bitmap;
        }
    }

    private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }
}