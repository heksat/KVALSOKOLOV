using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for CreateNewProgrammer.xaml
    /// </summary>
    public partial class CreateNewProgrammer : Page
    {
        public Programmers currentprgmst { get; set; }
        public CreateNewProgrammer()
        {
            InitializeComponent();
        }
        public CreateNewProgrammer(bool check) : this()
        {
            ChangedBut.IsEnabled = check;
        }
        public CreateNewProgrammer(Programmers prgmst) : this()
        {
            currentprgmst = prgmst;
        }

        private void AddProgrammer(object sender, RoutedEventArgs e)
        {
            try
            {
                float tempval = 0;
                if (Lname.Text.Trim() != String.Empty && Fname.Text.Trim() != String.Empty && Sname.Text.Trim() != String.Empty
                    && (Single.TryParse(SalaryBox.Text.Trim(), out tempval) || (SalaryBox.Text.ToUpper() == "NULL")))
                {
                    Programmers newprogrammer = new Programmers()
                    {
                        LName = Lname.Text.Trim(),
                        FName = Fname.Text.Trim(),
                        SName = Sname.Text.Trim()
                    };
                    if (SalaryBox.Text.ToUpper() == "NULL")
                    {
                        newprogrammer.Salary = null;
                    }
                    else
                    {
                        newprogrammer.Salary = tempval;
                    }
                    using (var db = new KVALSOKOLOVEntities())
                    {
                        db.Programmers.Add(newprogrammer);
                        db.SaveChanges();
                        MessageBox.Show("Был успешно добавлен");
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте правильность введенных значений");

                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChangedBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new KVALSOKOLOVEntities())
                {
                    db.Entry(currentprgmst).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных значений");
            }
        }
    }
}
