using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KVALSOKOLOV.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    /// 9 ВАР
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        static List<Requests> LoadeddatafromtextfileRequests = null;

        static List<Programmers> Loadeddatafromtextfileprogrammers = null;

        static List<Senders> LoadeddatafromtextfileSenders = null;

        static List<Programmers> Loadeddatafrombinfileprogrammers = null;


        private void GoViewProgrammers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewProgrammers());
        }
        public async Task WriteTextFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a text file";
            ofd.Filter = "Text Files | *.txt";
            if (ofd.ShowDialog() == true)
            {
                string writePath = ofd.FileName;
                try
                {
                    using (StreamWriter writer = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
                    {
                        using (var db = new KVALSOKOLOVEntities())
                        {
                            await writer.WriteLineAsync(db.Requests.Count().ToString());

                            foreach (var req in db.Requests)
                            {
                                await writer.WriteLineAsync(req.Name);
                                await writer.WriteLineAsync(req.description);
                                await writer.WriteLineAsync(req.Register.ToString());
                                await writer.WriteLineAsync(req.DateDone.ToString());
                                await writer.WriteLineAsync(req.Programmer.ToString());
                                await writer.WriteLineAsync(req.Sender.ToString());

                            }
                            await writer.WriteLineAsync(db.Senders.Count().ToString());

                            foreach (var sen in db.Senders)
                            {
                                await writer.WriteLineAsync(sen.id.ToString());
                                await writer.WriteLineAsync(sen.LName);
                                await writer.WriteLineAsync(sen.FName);
                                await writer.WriteLineAsync(sen.SName);
                                await writer.WriteLineAsync(sen.datereg.ToString());
                                await writer.WriteLineAsync(sen.Version);

                            }
                            await writer.WriteLineAsync(db.Programmers.Count().ToString());

                            foreach (var programmer in db.Programmers)
                            {
                                await writer.WriteLineAsync(programmer.id.ToString());
                                await writer.WriteLineAsync(programmer.LName);
                                await writer.WriteLineAsync(programmer.FName);
                                await writer.WriteLineAsync(programmer.SName);
                                await writer.WriteLineAsync(programmer.Salary.ToString());
                            }
                        }
                    }
                    MessageBox.Show("Запись выполнена");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public async Task ReadTextFile()
        {
            LoadeddatafromtextfileRequests = new List<Requests>();
            LoadeddatafromtextfileSenders = new List<Senders>();
            Loadeddatafromtextfileprogrammers = new List<Programmers>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a text file";
            ofd.Filter = "Text Files | *.txt";
            if (ofd.ShowDialog() == true)
            {
                string readPath = ofd.FileName;
                //try
                // {
                using (StreamReader reader = new StreamReader(readPath))
                {
                    using (var db = new KVALSOKOLOVEntities())
                    {
                        int count = Int32.Parse(await reader.ReadLineAsync());
                        for (int i = 0; i < count; i++)
                        {
                            var temp = new Requests();
                            temp.Name = await reader.ReadLineAsync();
                            temp.description = await reader.ReadLineAsync();
                            temp.Register = DateTime.Parse(await reader.ReadLineAsync());
                            DateTime tempdate;
                            if (DateTime.TryParse(await reader.ReadLineAsync(),out tempdate))
                            {
                                temp.DateDone = tempdate;
                            }
                            else
                            {
                                temp.DateDone = null;
                            }
                            temp.Programmer = Guid.Parse(await reader.ReadLineAsync());
                            temp.Sender = Guid.Parse(await reader.ReadLineAsync());
                            LoadeddatafromtextfileRequests.Add(temp);
                        }
                        int count2 = Int32.Parse(await reader.ReadLineAsync());
                        for (int i = 0; i < count2; i++)
                        {
                            var temp = new Senders();
                            temp.id = Guid.Parse(await reader.ReadLineAsync());//Int32.Parse(await reader.ReadLineAsync());
                            temp.LName = await reader.ReadLineAsync();
                            temp.FName = await reader.ReadLineAsync();
                            temp.SName = await reader.ReadLineAsync();
                            temp.datereg = DateTime.Parse(await reader.ReadLineAsync());
                            temp.Version = await reader.ReadLineAsync();
                            LoadeddatafromtextfileSenders.Add(temp);
                        }

                        int count3 = Int32.Parse(await reader.ReadLineAsync());
                        for (int i = 0; i < count3; i++)
                        {
                            var temp = new Programmers();
                            temp.id = Guid.Parse(await reader.ReadLineAsync());
                            temp.LName = await reader.ReadLineAsync();//Int32.Parse(await reader.ReadLineAsync());
                            temp.FName = await reader.ReadLineAsync();
                            temp.SName = await reader.ReadLineAsync();
                            temp.Salary = Single.TryParse(await reader.ReadLineAsync(), out var tempval) ? (float?)tempval : null;
                            Loadeddatafromtextfileprogrammers.Add(temp);
                        }
                        db.Programmers.RemoveRange(db.Programmers.ToList());
                        db.Programmers.AddRange(Loadeddatafromtextfileprogrammers);
                        db.SaveChanges();
                    }
                }

                // }
                // catch (Exception ex)
                // {
                //     MessageBox.Show(ex.Message);
                // }
            }
        }

        public void WriteBinFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a text file";
            ofd.Filter = "Text Files | *.bin";
            if (ofd.ShowDialog() == true)
            {
                string writePath = ofd.FileName;
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(writePath, FileMode.OpenOrCreate)))
                    {
                        using (var db = new KVALSOKOLOVEntities())
                        {
                            foreach (var programmer in db.Programmers)
                            {
                                writer.Write(programmer.LName);
                                writer.Write(programmer.FName);
                                writer.Write(programmer.SName);
                                if (programmer.Salary == null)
                                {
                                    writer.Write((float)-1);
                                }
                                else
                                {
                                    writer.Write((float)programmer.Salary);
                                }

                            }
                        }
                    }
                    MessageBox.Show("Запись выполнена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ReadBinFile()
        {
            Loadeddatafrombinfileprogrammers = new List<Programmers>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a text file";
            ofd.Filter = "Text Files | *.bin";
            if (ofd.ShowDialog() == true)
            {
                string readPath = ofd.FileName;
                try
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(readPath, FileMode.Open)))
                    {
                        using (var db = new KVALSOKOLOVEntities())
                        {
                            while (reader.PeekChar() > -1)
                            {
                                var temp = new Programmers();
                                temp.LName = reader.ReadString();
                                temp.FName = reader.ReadString();
                                temp.SName = reader.ReadString();
                                var tempsalary = reader.ReadSingle();
                                if (tempsalary == -1)
                                {
                                    temp.Salary = null;
                                }
                                else
                                {
                                    temp.Salary = tempsalary;
                                }
                                Loadeddatafrombinfileprogrammers.Add(temp);
                            }
                            db.Programmers.RemoveRange(db.Programmers.ToList());
                            db.Programmers.AddRange(Loadeddatafrombinfileprogrammers);
                            db.SaveChanges();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async void SaveText_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => WriteTextFile());

        }

        private async void LoadText_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => ReadTextFile());

        }

        private async void SaveBin_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => WriteBinFile());

        }

        private async void LoadBin_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => ReadBinFile());

        }
    }

}
