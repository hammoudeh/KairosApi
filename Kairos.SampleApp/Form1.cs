using KaiorsApi;
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SampleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            KairosClient c = new KairosClient("your_app_id", "your_app_key");
            string s = c.recognize("http://www.link.to/your/image.jpg", "gallery1",null,null,null,"10");

            MessageBox.Show(s);
              
        }
    }
}
