using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;

namespace SMS_Notifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();
            port.PortName = "COM" + textBox1.Text;
            port.WriteTimeout = 500;
            port.ReadTimeout = 500;
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Handshake = Handshake.RequestToSend;
            port.DtrEnable = true;
            port.RtsEnable = true;
            port.NewLine = System.Environment.NewLine;
            try
            {
                port.Open();
            }
            catch
            {
                MessageBox.Show("Не могу открыть порт");
                return;
            }
            try
            {
                System.Threading.Thread.Sleep(500);
                port.WriteLine("AT+CMGF=1");
                port.WriteLine("AT+CMGS=" + (char)(34) + textBox2.Text + (char)(34) + ",145");
                port.WriteLine(textBox3.Text + System.Environment.NewLine + (char)(26));
            }
            catch
            {
                MessageBox.Show("Нету связи с модемом");
                port.Close();
                return;
            }
            MessageBox.Show("SMS отправлено, вроде бы... :)");
            port.Close();
        }
    }
}
