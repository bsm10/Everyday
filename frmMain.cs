using System;
using System.Drawing;
using System.Windows.Forms;
using Everyday;

//using System.Runtime.Serialization.Json;
//using System.Web;

namespace Everyday
{
    //  '"http://api.everyday.mk.ua/"

    public partial class frmMain : Form
    {
        GetEvents events;
        
        //Everyday eday;
        public frmMain()
        {
            InitializeComponent();

            pbxKlient.Image = Everyday.UserImg;
                        
            events = Everyday.GetEventsByData(monthCalendar1.TodayDate.ToString("yyyy-MM-dd"), monthCalendar1.TodayDate.ToString("yyyy-MM-dd"));
            FillData(events);
        }


         private void FillData(GetEvents events)
         {
             ImageList imageListSmall = new ImageList();
             ImageList imageListLarge = new ImageList();

             listView1.Clear();
             listView1.Columns.Add("Мероприятия", 130, HorizontalAlignment.Left);
             listView1.Columns.Add("Продукты", 200, HorizontalAlignment.Left);
             foreach (Event ev in events.events)
             {
                 ListViewItem item = new ListViewItem(ev.event_name.ToString(), 0);
                 item.Checked = ev.confirmed == 1 ? true : false;

                 Bitmap bmp = Everyday.GetResponse(Everyday.SERVER_IMG + ev.img, true) as Bitmap;
                 if (bmp != null) imageListSmall.Images.Add(bmp);
                 
                 ListViewItem.ListViewSubItemCollection lvi = new ListViewItem.ListViewSubItemCollection(item);
                 if (ev.details != null && ev.details.items != null)
                 {
                    foreach (Item it in ev.details.items)
                    {
                        lvi.Add(it.caption);
                    }
                 }  
                 listView1.Items.AddRange(new ListViewItem[] { item });
             }
             //Initialize the ImageList objects with bitmaps.
             //imageListSmall.Images.Add(pbxKlient.Image);//Bitmap.FromFile("C:\\MySmallImage1.bmp"));
             //imageListSmall.Images.Add(pbxKlient.Image);//Bitmap.FromFile("C:\\MySmallImage2.bmp"));
             //imageListLarge.Images.Add(pbxKlient.Image);//Bitmap.FromFile("C:\\MyLargeImage1.bmp"));
             //imageListLarge.Images.Add(pbxKlient.Image);//Bitmap.FromFile("C:\\MyLargeImage2.bmp"));

             //Assign the ImageList objects to the ListView.
             //listView1.LargeImageList = imageListLarge;
             //listView1.SmallImageList = imageListSmall;
             listView1.SmallImageList = imageListSmall;
             
         }

         private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
         {
             events = Everyday.GetEventsByData(e.Start.ToString("yyyy-MM-dd"), e.End.ToString("yyyy-MM-dd"));
             FillData(events);
         }
    }


}
