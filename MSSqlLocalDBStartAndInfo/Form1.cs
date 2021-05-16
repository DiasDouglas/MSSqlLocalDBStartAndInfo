using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MSSqlLocalDBStartAndInfo
{
    public partial class Form1 : Form
    {
        private Process Process { get; set; }
        private string LocalDBFile 
        { 
            get
            {
                return "SqlLocalDB.exe";
            }
        }

        private string LocalDBDefaultInfoArgument
        {
            get
            {
                return "i MSSQLLocalDB";
            }
        }

        private string LocalDBDefaultStartArgument
        {
            get
            {
                return "s MSSQLLocalDB";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process = new Process();
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            InitializeProcess(LocalDBDefaultInfoArgument);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeProcess(LocalDBDefaultStartArgument);
        }

        private void InitializeProcess(string arguments)
        {
            commandTextBox.Text = $"{LocalDBFile} {arguments}";

            Process.StartInfo.FileName = LocalDBFile;

            Process.StartInfo.Arguments = arguments;
            Process.Start();
            Process.WaitForExit();

            string output = FormatOutput();

            outputTextBox.Text = output;
        }

        private string FormatOutput()
        {
            string[] splittedOutput = Process.StandardOutput.ReadToEnd().Split('\n', '\r').Where(s => s.Length > 0).ToArray();
            string output = "";

            foreach (var line in splittedOutput)
            {
                output += $"{line}\n";
            }

            return output;
        }
    }
}
