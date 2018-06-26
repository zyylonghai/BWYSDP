using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BWYSDP.Controls
{
    public class LibTabControl : TabControl
    {
         private int iconWidth = 16;  
       private int iconHeight = 16;  
       private Image icon = null;  
       private Brush biaocolor = Brushes.Silver; //选项卡的背景色  
       //private Form_paint father;//父窗口，即绘图界面，为的是当选项卡全关后调用父窗口的dispose事件关闭父窗口  
       //private AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl1;
       public LibTabControl()  
           : base()  
       {  
           //this.axDrawingControl1 = axDrawingControl;  
           this.ItemSize = new Size(250, 25); //设置选项卡标签的大小,可改变高不可改变宽    
           this.Appearance = TabAppearance.Normal; //选项卡的显示模式   
           this.DrawMode = TabDrawMode.OwnerDrawFixed;
           icon = Properties.Resources.close;  
           iconWidth = icon.Width; 
           iconHeight = icon.Height;  
       }

       /// <summary>  
       /// 重写的绘制事件  
       /// </summary>  
       /// <param name="e"></param>  
       protected override void OnDrawItem(DrawItemEventArgs e)//重写绘制事件。  
       {
           Graphics g = e.Graphics;
           Rectangle r = GetTabRect(e.Index);
           if (e.Index == this.SelectedIndex)    //当前选中的Tab页，设置不同的样式以示选中  
           {
               Brush selected_color = Brushes.Gold; //选中的项的背景色  
               g.FillRectangle(selected_color, r); //改变选项卡标签的背景色   
               string title = this.TabPages[e.Index].Text + "   ";
               g.DrawString(title, this.Font, new SolidBrush(Color.Black), new PointF(r.X + 3, r.Y + 6));//PointF选项卡标题的位置   
               r.Offset(r.Width - iconWidth - 3, 2);
               g.DrawImage(icon, new Point(r.X - 2, r.Y + 2));//选项卡上的图标的位置 fntTab = new System.Drawing.Font(e.Font, FontStyle.Bold);  
           }
           else//非选中的  
           {
               g.FillRectangle(biaocolor, r); //改变选项卡标签的背景色   
               string title = this.TabPages[e.Index].Text + "   ";
               g.DrawString(title, this.Font, new SolidBrush(Color.Black), new PointF(r.X + 3, r.Y + 6));//PointF选项卡标题的位置   
               r.Offset(r.Width - iconWidth - 3, 2);
               g.DrawImage(icon, new Point(r.X - 2, r.Y + 2));//选项卡上的图标的位置   
           }
       } 
    }
}
