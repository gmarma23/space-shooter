using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SpaceShooter.gui
{
    public partial class CustomFrame : Form
    {
        public CustomFrame()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Space Shooter";
            AutoSize = false;
            MaximizeBox = false;
            BackColor = Color.Black;
            Size = new Size(400, 500);
        }
    }
}
