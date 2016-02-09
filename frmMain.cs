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
            txtUserInfo.Text = everyday.getUserInfo.UserF + "\r\n" + everyday.getUserInfo.UserI +
                               "\r\n" + everyday.getUserInfo.UserO + "\r\nДата регистрации: " + everyday.getUserInfo.UserDateReg;
            //everyday.getEvents.a_day_date

            LoadTree(everyday,treeView1);
        }

        public void LoadTree(Everyday evd, TreeView TrV) 
        {
            TrV.BeginUpdate();
            TrV.Nodes.Clear();
            TreeNode Node = new TreeNode();
            Node.Text=evd.getUserInfo.UserI;
            //TreeNode NodeChild = new TreeNode();
            //        TrV.SelectedNode.Nodes.Add(Node);
            //        TrV.SelectedNode = Node;
            //        NodeChild.Text = evd.getUserInfo.UserF;
            //        TrV.SelectedNode.Nodes.Add(NodeChild);
            //        TrV.SelectedNode = NodeChild;
            TrV.CollapseAll();
            //TrV.Nodes[0].Expand();
            TrV.EndUpdate();
        }

    }


}
