using System;
using System.Drawing;
using System.Windows.Forms;
//using System.Runtime.Serialization.Json;
//using System.Web;

namespace Everyday
{
    //  '"http://api.everyday.mk.ua/"

    public partial class frmMain : Form
    {
        public frmMain(Everyday everyday)
        {
            InitializeComponent();

            pbxKlient.Image = everyday.UserImg;

            

        }

    }


}
