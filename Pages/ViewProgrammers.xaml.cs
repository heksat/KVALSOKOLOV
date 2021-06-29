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
    /// Interaction logic for ViewProgrammers.xaml
    /// </summary>
    public partial class ViewProgrammers : Page
    {
        public ViewProgrammers()
        {
            InitializeComponent();
            using (var db = new KVALSOKOLOVEntities())
            {
                ViewPrgms.ItemsSource = db.Programmers.ToList();
            }
        }



        

        

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChangedProg_Click(object sender, RoutedEventArgs e)
        {
            Programmers prgmst = ((Programmers)ViewPrgms.SelectedItem);
            using (var db = new KVALSOKOLOVEntities())
            {
                NavigationService.Navigate(new CreateNewProgrammer(prgmst));
            }
        }

        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNewProgrammer(false));
        }
    }
}
