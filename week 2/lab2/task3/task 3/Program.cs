using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _17072011_home
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();

            Bitmap img1 = new Bitmap("icon1.bmp");
            Bitmap img2 = new Bitmap("Drive01.ico");

            listView1.View = View.Details;
            listView1.SmallImageList = new ImageList();
            listView1.Columns.Add("File name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Path", 205, HorizontalAlignment.Left);
            listView1.Columns.Add("Date", 100, HorizontalAlignment.Left);

            ToolStripComboBox combo = new ToolStripComboBox();
            /*combo.Items.Add(@"C:\");
            combo.Items.Add(@"D:\");
            combo.SelectedIndex = 0;
            combo.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            this.MainMenuStrip.Items.Add(combo);*/

            string[] drives = Directory.GetLogicalDrives();

            foreach (string str in Directory.GetLogicalDrives())
            {
                string s;
                s = str.Replace('\\', ' ');
                combo.Items.Add(str);
                /*TreeNode node = new TreeNode(str, 1, 1);
                treeView1.Nodes.Add(node);
                FillByDirectories(node);*/
            }
            combo.SelectedIndex = 0;
            combo.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            this.MainMenuStrip.Items.Add(combo);



            //Добавление Логических дисков в Tree

            // Метод для получения списка логических дисков
            //string[] drives = Directory.GetLogicalDrives();
            // Перебор списка дисков

            /*foreach (string drive in drives)
            {
                // Создание конкретного узла и назначение иконок
                TreeNode node = new TreeNode(drive, 1, 1);
                // Добавили готовый узел к дереву   
                treeView1.Nodes.Add(node);
                // Заполнение узлов с дисками
                FillByDirectories(node);
            }*/
        }

        void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.SmallImageList.Images.Clear();                                //Очищаем список существующих иконок
            listView1.Items.Clear();                                                //Очищаем listView
            treeView1.Nodes.Clear();                                                //Очищаем treeView
            string str = ((ToolStripComboBox)sender).SelectedItem.ToString();
            path = str;
            DirectoryInfo dir = new DirectoryInfo(str);
            FileInfo f = new FileInfo(str);
            FileInfo[] d = dir.GetFiles();
            DirectoryInfo[] g = dir.GetDirectories();
            for (int i = 0; i < d.Length; i++)
            {
                ListViewItem item = new ListViewItem(d[i].Name);
                item.SubItems.Add(d[i].FullName);
                item.SubItems.Add(f.CreationTime.ToString());
                listView1.Items.Add(item);
                this.Icon = Icon.ExtractAssociatedIcon(d[i].FullName);
                listView1.SmallImageList.Images.Add(this.Icon);
                item.ImageIndex = i;
            }

            for (int i = 0; i < g.Length; i++)
            {
                TreeNode node = new TreeNode(g[i].Name);
                treeView1.Nodes.Add(node);
                FillByDirectories(node);
            }

        }

        // Метод для заполнения узлов дерева содержимым каталога    
        private void FillByDirectories(TreeNode node)
        {
            try
            {
                // В node.FullPath - находится полный путь к ветке
                DirectoryInfo dirinfo = new DirectoryInfo(node.FullPath);
                // Получение информации о каталогах
                DirectoryInfo[] dirs = dirinfo.GetDirectories();
                // Обработка информации
                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode tree = new TreeNode(dir.Name, 0, 0);
                    node.Nodes.Add(tree);
                }
            }
            // Исключение будет генерироваться  например для дисковода, если там нет
            // диска    
            catch { }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                string ext = Path.GetExtension(lvi.Text).ToLower();
                if (ext == ".bmp" || ext == ".jpg" || ext == "gif" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                {
                    FileInfo f = new FileInfo(lvi.Text);
                    string name = f.Name;
                    string FullPath = Path.Combine(path, name);
                    pictureBox1.Image = Bitmap.FromFile(FullPath);
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                string ext = Path.GetExtension(lvi.Text).ToLower();
                if (ext == ".bmp" || ext == ".jpg" || ext == "gif" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                {
                    FileInfo f = new FileInfo(lvi.Text);
                    string name = f.Name;
                    string FullPath = Path.Combine(path, name);
                    Form2 f2 = new Form2(FullPath);
                    f2.Height = pictureBox1.Image.Height;
                    f2.Width = pictureBox1.Image.Width;
                    f2.Show();
                }
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);// Получаем массив адресов перетаскиваемых файлов
                for (int i = 0; i < str.Length; i++)// Проходимся по каждому файлу и выводим его в новом дочернем окне
                {
                    string ext = Path.GetExtension(str[i]);// вычисляем расширение файла
                    string path = str[i];
                    DirectoryInfo dir = new DirectoryInfo(path);
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.ImageIndex = 0;
                    item.SubItems.Add(dir.FullName);
                    item.SubItems.Add(dir.CreationTime.ToString());
                    Icon tt = Icon.ExtractAssociatedIcon(str[i]);
                    listView1.SmallImageList.Images.Add(tt);
                    item.ImageIndex = i;
                    listView1.Items.Add(item);
                }
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy) != 0)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    FillByDirectories(node);
                }
            }
            catch { }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            //listView1.Clear();
            /*string path = treeView1.SelectedNode.FullPath.ToString();
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo f = new FileInfo(path);
            FileInfo[] d = dir.GetFiles();
            for (int i = 0; i < d.Length; i++)
            {
                ListViewItem item = new ListViewItem(d[i].Name);
                item.SubItems.Add(d[i].FullName);
                item.SubItems.Add(f.CreationTime.ToString());
                listView1.Items.Add(item);
                this.Icon = Icon.ExtractAssociatedIcon(d[i].FullName);
                listView1.SmallImageList.Images.Add(this.Icon);
                item.ImageIndex = i;
            }*/
        }
    }
}