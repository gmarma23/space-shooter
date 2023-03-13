using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceShooter.gui
{
    public partial class HighscoresFrame : Form
    {
        public HighscoresFrame()
        {
            InitializeComponent();
            FormClosed += AppManager.OnSubFrameClose;
        }
    }
}
