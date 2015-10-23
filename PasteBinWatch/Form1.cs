using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteBinWatch
{
    public partial class Form1 : Form
    {
        public string lastURL = "http://pastebin.com/";
        public WebClient client = new WebClient();
        public Form1()
        {
            InitializeComponent();
            wb1.ScriptErrorsSuppressed = true;
            wb1.Navigate(lastURL);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            wb1.Navigate(lastURL);
            while (wb1.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            string html = wb1.DocumentText;
            html = html.Substring(html.IndexOf("content_right_menu") + 18);
            html = html.Substring(html.IndexOf("right_menu"));
            html = html.Substring(html.IndexOf("a href=\"/") + 8);
            html = html.Substring(0, html.IndexOf("\""));            
            lastURL = "http://pastebin.com" + html;
            this.Text = "PasteBin Watcher - " + lastURL;
            HtmlElement el = wb1.Document.GetElementById("paste_code");
            rtb1.Text = el.InnerText;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !toolStripButton1.Checked;
        }
    }
}
