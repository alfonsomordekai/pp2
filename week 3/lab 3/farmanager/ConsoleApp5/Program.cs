using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FarManager2
{
    class Layer
    {
        public FileSystemInfo[] Content
        {
            get;
            set;
        }
        int selectedItem;
        public int SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (value < 0)
                {
                    selectedItem = Content.Length - 1;
                }
                else if (value >= Content.Length)
                {
                    selectedItem = 0;
                }
                else
                {
                    selectedItem = value;
                }
            }
        }
        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < Content.Length; ++i)
            {
                if (i == SelectedItem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(Content[i].Name);
            }
        }
    }
    enum FarMode
    {
        FileView,
        DirectoryView
    }
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo root = new DirectoryInfo(@"D:\test");
            Stack<Layer> history = new Stack<Layer>();
            FarMode farMode = FarMode.DirectoryView;
            history.Push(
                new Layer
                {
                    Content = root.GetFileSystemInfos(),
                    SelectedItem = 0
                });
            int a = 0;
            string gfh = Convert.ToString(root);
            while (a == 0)
            {

                if (farMode == FarMode.DirectoryView)
                {
                    history.Peek().Draw();
                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        history.Peek().SelectedItem--;
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SelectedItem++;
                        break;
                    case ConsoleKey.Enter:
                        int x = history.Peek().SelectedItem;
                        FileSystemInfo fileSystemInfo = history.Peek().Content[x];
                        if (fileSystemInfo.GetType() == typeof(DirectoryInfo))
                        {
                            DirectoryInfo d = fileSystemInfo as DirectoryInfo;
                            history.Push(new Layer { Content = d.GetFileSystemInfos(), SelectedItem = 0 });
                        }
                        else
                        {
                            farMode = FarMode.FileView;
                            using (FileStream fs = new FileStream(fileSystemInfo.FullName, FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd());
                                }
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (farMode == FarMode.DirectoryView)
                        {
                            history.Pop();
                        }
                        else if (farMode == FarMode.FileView)
                        {
                            farMode = FarMode.DirectoryView;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case ConsoleKey.Delete:
                        int x2 = history.Peek().SelectedItem;
                        FileSystemInfo fileSystemInfo2 = history.Peek().Content[x2];
                        if (fileSystemInfo2.GetType() == typeof(DirectoryInfo))
                        {
                            DirectoryInfo d = fileSystemInfo2 as DirectoryInfo;
                            Directory.Delete(fileSystemInfo2.FullName, true);
                            history.Peek().Content = d.Parent.GetFileSystemInfos();
                        }
                        else
                        {
                            FileInfo f = fileSystemInfo2 as FileInfo;
                            File.Delete(fileSystemInfo2.FullName);
                            history.Peek().Content = f.Directory.GetFileSystemInfos();
                        }
                        history.Peek().SelectedItem--;
                        break;
                    case ConsoleKey.R:
                        int x3 = history.Peek().SelectedItem;
                        FileSystemInfo fileSystemInfo3 = history.Peek().Content[x3];
                        //string o = fileSystemInfo3.Name.ToString();
                        Console.Clear();
                        string ttt = Console.ReadLine();
                        if (fileSystemInfo3.GetType() == typeof(DirectoryInfo))
                        {
                            DirectoryInfo dfd = fileSystemInfo3 as DirectoryInfo;
                            Console.Clear();

                            try
                            {
                                dfd.MoveTo(Path.Combine(dfd.Parent.FullName, ttt));
                                history.Peek().Content = dfd.Parent.GetFileSystemInfos();
                            }
                            catch
                            {
                                Console.Write("this name already exists, try again");
                            }
                        }
                        else
                        {
                            ttt = ttt + ".txt";
                            FileInfo ddd = fileSystemInfo3 as FileInfo;
                            try
                            {
                                File.Move(fileSystemInfo3.FullName, fileSystemInfo3.FullName.Replace(fileSystemInfo3.Name, ttt));
                            }
                            catch
                            {
                                Console.Write("this name already exists, try again");
                            }
                            history.Peek().Content = ddd.Directory.GetFileSystemInfos();
                        }
                        break;
                    case ConsoleKey.Escape:
                        a = 1;
                        break;
                }
            }
        }
    }
}