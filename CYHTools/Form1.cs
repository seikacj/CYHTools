using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CYHTools
{
    public partial class Form1 : Form
    {
        private int isUp = 0;
        private int maxnum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.txtSubNum.Text = (100 - this.toInt(this.txtAddNum.Text)).ToString();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.txtAddNum.Text = (100 - this.toInt(this.txtSubNum.Text)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dgv.DataSource = null;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\100以内加减";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(path + @"\题目" + DateTime.Now.ToString("yyMMddHHmmss") + ".csv", FileMode.Create))
            {
                this.isUp = this.radioButton1.Checked ? 0 : (this.radioButton2.Checked ? 1 : 2);//0混合 1进位 2不进位
                int totalNum = this.toInt(this.txtTotalNum.Text);//总体题数
                int addNum = (totalNum * this.toInt(this.txtAddNum.Text)) / 100;//加法数量
                int subNum = (totalNum * this.toInt(this.txtSubNum.Text)) / 100;//减法数量
                int rowNum = (int)Math.Ceiling((decimal)(this.toInt(this.txtTotalNum.Text) / 4M));//行数
                List<string> list = new List<string>();
                foreach (RadioButton button in this.groupBox3.Controls)
                {
                    if (button.Checked)
                    {
                        switch (button.Name)
                        {
                            case "radioButton6":
                                this.maxnum = 100;
                                break;

                            case "radioButton7":
                                this.maxnum = 90;
                                break;

                            case "radioButton8":
                                this.maxnum = 80;
                                break;

                            case "radioButton9":
                                this.maxnum = 70;
                                break;

                            case "radioButton10":
                                this.maxnum = 60;
                                break;

                            case "radioButton11":
                                this.maxnum = 50;
                                break;

                            case "radioButton12":
                                this.maxnum = 40;
                                break;

                            case "radioButton13":
                                this.maxnum = 30;
                                break;

                            case "radioButton14":
                                this.maxnum = 20;
                                break;

                            case "radioButton15":
                                this.maxnum = 10;
                                break;
                        }
                        break;
                    }
                }
                int max = 0;//第一位
                int min = 0;//第一位
                int min1 = 0;
                int min2 = 0;
                string item = "";
                for (int i = 0; i < addNum; i++)
                {

                    if (this.maxnum == 10)
                    {
                        max = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10);
                        min = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10 - max);
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        continue;
                    }
                    item = "";
                    int count = 0;//重复次数计数器
                    //防止题目重复 5次后强制退出
                    while ((string.IsNullOrEmpty(item) || (list.Contains(item) && (this.isUp != 1))) && (count < 5))
                    {
                        switch (this.isUp)
                        {
                            case 1:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(1, this.maxnum - 10);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next((this.maxnum - max) % 10, 10);
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, (this.maxnum - max) / 10) * 10;
                                min = min1 + min2;
                                break;
                            case 2:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, this.maxnum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, (((this.maxnum - max) % 10) == 0) ? 10 : ((this.maxnum - max) % 10));
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, (this.maxnum - max) / 10) * 10;
                                min = min1 + min2;
                                break;
                            default:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, this.maxnum);
                                min = new Random(Guid.NewGuid().GetHashCode()).Next(1, this.maxnum - max);
                                break;
                        }
                        item = string.Format("{0}+{1}=", max, min);
                        list.Add(item);
                        count++;
                    }
                }


                for (int i = 0; i < subNum; i++)
                {
                    if (this.maxnum == 10)
                    {
                        max = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10);
                        min = new Random(Guid.NewGuid().GetHashCode()).Next(0, max);
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        continue;
                    }

                    item = "";
                    int count = 0;//重复次数计数器
                    //防止题目重复 5次后强制退出
                    while ((string.IsNullOrEmpty(item) || (list.Contains(item) && (this.isUp != 1))) && (count < 5))
                    {
                        switch (this.isUp)
                        {
                            case 1:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, this.maxnum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next((max % 10) + 1, 10);
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, max / 10) * 10;
                                min = min1 + min2;
                                break;

                            case 2:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, this.maxnum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, ((max % 10) == 0) ? 10 : (max % 10));
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, max / 10) * 10;
                                min = min1 + min2;
                                break;

                            default:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, this.maxnum);
                                min = new Random(Guid.NewGuid().GetHashCode()).Next(1, max);
                                break;
                        }
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        count++;
                    }
                }

                List<string> list2 = (from c in list
                                      orderby Guid.NewGuid()
                                      select c).ToList<string>();

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    int count = 0;//题目计数器
                    for (int i = 0; i < rowNum; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (count < totalNum)
                            {
                                writer.Write(list2[count] + ",");
                            }
                            count++;
                        }
                        writer.Write("\r\n");
                    }
                }

                DataTable table = new DataTable();
                table.Columns.Add("列1");
                table.Columns.Add("列2");
                table.Columns.Add("列3");
                table.Columns.Add("列4");
                for (int i = 0; i < rowNum; i++)
                {
                    DataRow row = table.NewRow();
                    row[0] = ((i * 4) < this.toInt(this.txtTotalNum.Text)) ? list2[i * 4] : "";
                    row[1] = (((i * 4) + 1) < this.toInt(this.txtTotalNum.Text)) ? list2[(i * 4) + 1] : "";
                    row[2] = (((i * 4) + 2) < this.toInt(this.txtTotalNum.Text)) ? list2[(i * 4) + 2] : "";
                    row[3] = (((i * 4) + 3) < this.toInt(this.txtTotalNum.Text)) ? list2[(i * 4) + 3] : "";
                    table.Rows.Add(row);
                }
                this.dgv.DataSource = table;
            }

        }


        private int toInt(string num)
        {
            int result = 0;
            int.TryParse(num, out result);
            return result;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                new PrintPreviewDialog { Document = this.printDocument1, WindowState = FormWindowState.Maximized }.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("打印错误，请检查打印设置！");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string titleName = string.Format("{0}以内{1}加减法", this.maxnum, (this.isUp == 0) ? "混合" : ((this.isUp == 1) ? "进退位" : "不退位"));
            string titleName2 = "班级___________姓名_________";

            //标题字体
            Font titleFont = new Font("微软雅黑", 16, FontStyle.Bold);
            //内容字体
            Font contentFont = new Font("宋体", 12, FontStyle.Regular);
            //标题尺寸
            SizeF titleSize = e.Graphics.MeasureString(titleName, titleFont, e.MarginBounds.Width);
            //x坐标
            int x = e.MarginBounds.Left;
            //y坐标
            int y = Convert.ToInt32(e.MarginBounds.Top - titleSize.Height);
            //边距以内纸张宽度
            int pagerWidth = e.MarginBounds.Width;
            //画标题
            e.Graphics.DrawString(titleName, titleFont, Brushes.Black, x + (pagerWidth - titleSize.Width) / 2, y);
            y += (int)titleSize.Height;
            if (titleName2 != null && titleName2 != "")
            {

                //画第二标题
                e.Graphics.DrawString(titleName2, dgv.Font, Brushes.Black, x + (pagerWidth - titleSize.Width) / 2 + 200, y);
                //第二标题尺寸
                SizeF titleSize2 = e.Graphics.MeasureString(titleName2, dgv.Font, e.MarginBounds.Width);
                y += (int)titleSize2.Height + 20;

            }

            //表头高度
            //int headerHeight = 0;
            //纵轴上 内容与线的距离
            int padding = 6;
            //所有显示列的宽度
            int columnsWidth = 0;
            //计算所有显示列的宽度
            foreach (DataGridViewColumn column in dgv.Columns)
            {

                //隐藏列返回
                if (!column.Visible) continue;
                //所有显示列的宽度
                columnsWidth += column.Width;
            }

            ////计算表头高度
            //foreach (DataGridViewColumn column in dgv.Columns)
            //{

            //    //列宽
            //    int columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));
            //    //表头高度
            //    int temp = (int)e.Graphics.MeasureString(column.HeaderText, column.InheritedStyle.Font, columnWidth).Height + 2 * padding;
            //    if (temp > headerHeight) headerHeight = temp;
            //}

            ////画表头

            //foreach (DataGridViewColumn column in dgv.Columns)
            //{

            //    //隐藏列返回
            //    if (!column.Visible) continue;
            //    //列宽
            //    int columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));
            //    //内容居中要加的宽度
            //    float cenderWidth = (columnWidth - e.Graphics.MeasureString(column.HeaderText, column.InheritedStyle.Font, columnWidth).Width) / 2;
            //    if (cenderWidth < 0) cenderWidth = 0;
            //    //内容居中要加的高度
            //    float cenderHeight = (headerHeight + padding - e.Graphics.MeasureString(column.HeaderText, column.InheritedStyle.Font, columnWidth).Height) / 2;
            //    if (cenderHeight < 0) cenderHeight = 0;
            //    //画背景
            //    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(x, y, columnWidth, headerHeight));
            //    //画边框
            //    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x, y, columnWidth, headerHeight));
            //    ////画上边线

            //    //e.Graphics.DrawLine(Pens.Black, x, y, x + columnWidth, y);

            //    ////画下边线

            //    //e.Graphics.DrawLine(Pens.Black, x, y + headerHeight, x + columnWidth, y + headerHeight);

            //    ////画右边线

            //    //e.Graphics.DrawLine(Pens.Black, x + columnWidth, y, x + columnWidth, y + headerHeight);

            //    //if (x == e.MarginBounds.Left)

            //    //{

            //    //    //画左边线

            //    //    e.Graphics.DrawLine(Pens.Black, x, y, x, y + headerHeight);

            //    //}

            //    //画内容
            //    e.Graphics.DrawString(column.HeaderText, column.InheritedStyle.Font, new SolidBrush(column.InheritedStyle.ForeColor), new RectangleF(x + cenderWidth, y + cenderHeight, columnWidth, headerHeight));
            //    x += columnWidth;

            //}

            //x = e.MarginBounds.Left;
            //y += headerHeight;
            int rowIndex = 0;
            while (rowIndex < dgv.Rows.Count)
            {

                DataGridViewRow row = dgv.Rows[rowIndex];
                if (row.Visible)
                {

                    int rowHeight = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        DataGridViewColumn column = dgv.Columns[cell.ColumnIndex];
                        if (!column.Visible || cell.Value == null) continue;
                        int tmpWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));
                        int temp = (int)e.Graphics.MeasureString(cell.Value.ToString(), column.InheritedStyle.Font, tmpWidth).Height + 3 * padding + 4;
                        if (temp > rowHeight) rowHeight = temp;
                    }

                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        DataGridViewColumn column = dgv.Columns[cell.ColumnIndex];
                        if (!column.Visible) continue;
                        int columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x, y, columnWidth, rowHeight));

                        if (cell.Value != null)
                        {

                            //内容居中要加的宽度

                            float cenderWidth = (columnWidth - e.Graphics.MeasureString(cell.Value.ToString(), cell.InheritedStyle.Font, columnWidth).Width) / 2;

                            if (cenderWidth < 0) cenderWidth = 0;

                            //内容居中要加的高度

                            float cenderHeight = (rowHeight + padding - e.Graphics.MeasureString(cell.Value.ToString(), cell.InheritedStyle.Font, columnWidth).Height) / 2;

                            if (cenderHeight < 0) cenderHeight = 0;

                            ////画下边线

                            //e.Graphics.DrawLine(Pens.Black, x, y + rowHeight, x + columnWidth, y + rowHeight);

                            ////画右边线

                            //e.Graphics.DrawLine(Pens.Black, x + columnWidth, y, x + columnWidth, y + rowHeight);

                            //if (x == e.MarginBounds.Left)

                            //{

                            //    //画左边线

                            //    e.Graphics.DrawLine(Pens.Black, x, y, x, y + rowHeight);

                            //}

                            //画内容

                            e.Graphics.DrawString(cell.Value.ToString(), contentFont, new SolidBrush(cell.InheritedStyle.ForeColor), new RectangleF(x, y + cenderHeight, columnWidth, rowHeight));

                        }

                        x += columnWidth;

                    }

                    x = e.MarginBounds.Left;

                    y += rowHeight;

                    //if (page == 1) rowsPerPage++;

                    ////打印下一页

                    //if (y + rowHeight > e.MarginBounds.Bottom)
                    //{

                    //    e.HasMorePages = true;

                    //    break;

                    //}

                }

                rowIndex++;

            }

            ////页脚
            //string footer = " 第 " + page + " 页，共 " + Math.Ceiling(((double)dgv.Rows.Count / rowsPerPage)).ToString() + " 页";
            ////画页脚
            //e.Graphics.DrawString(footer, dgv.Font, Brushes.Black, x + (pagerWidth - e.Graphics.MeasureString(footer, dgv.Font).Width) / 2, e.MarginBounds.Bottom);
            //page++;
        }


    }
}
