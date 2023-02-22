using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Zametochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DataModel> Notes = new List<DataModel>();
        List<DataModel> SortNote = new List<DataModel>();
        List<DataModel> FictList = new List<DataModel>();
        int idtrek;
        string file = "Zametochka";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "Zametochka";
            bool fileExist = File.Exists(path);
            if (fileExist)
            {
                Notes = Serializator.myDeser<List<DataModel>>(file);
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            Create();
            spisok.ItemsSource = DataSort();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SaveOrDelete((int)enumss.NeedTpDelete);
            spisok.ItemsSource = DataSort();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveOrDelete((int)enumss.NeedToSave);
            spisok.ItemsSource = DataSort();
        }

        private void spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int ind = spisok.SelectedIndex;
                nameField.Text = SortNote[ind].Name;
                desField.Text = SortNote[ind].Descr;
                Flag.IsChecked = SortNote[ind].ReadyOrNot;
            }
            catch (ArgumentOutOfRangeException)
            {
                ///за костыль соре
            }
        }

        private void dtpic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            spisok.ItemsSource = DataSort();
        }

        private List<string> DataSort()
        {
            DateTime date = Convert.ToDateTime(dtpic.Text);
            SortNote = Notes.Where(item => item.CrDate == date).ToList();
            List<string> names = new List<string>();
            foreach (var item in SortNote)
            {
                names.Add(item.Name);
            }
            return names;
        }

        private void Create()
        {
            idtrek = Notes.Count();
            if (idtrek == (int)enumss.ListIsEmpty)
            {
                NoteGen((int)enumss.ListIsEmpty);
            }
            else
            {
                NoteGen((int)enumss.ListContains);
            }
        }

        private void NoteGen(int param)
        {
            DataModel item = new DataModel()
            {
                Name = nameField.Text,
                CrDate = Convert.ToDateTime(dtpic.Text),
                Descr = desField.Text,
                ReadyOrNot = Convert.ToBoolean(Flag.IsChecked),
                id = idtrek + param
            };
            Notes.Add(item);
            Serializator.mySer(Notes, file);
        }

        private void SaveOrDelete(int du)
        {
            int ind = spisok.SelectedIndex;
            if (du == (int)enumss.NeedToSave)
            {
                SortNote[ind].Name = nameField.Text;
                SortNote[ind].Descr = desField.Text;
                SortNote[ind].ReadyOrNot = Convert.ToBoolean(Flag.IsChecked);
            }
            FictList = Notes.Where(item => item.id == SortNote[ind].id).ToList();
            int sch = 0;
            int poz = 0;
            foreach (var item in Notes)
            {
                if (item.id == FictList[0].id)
                {
                    poz = sch; ///Ищу индекс заметки с таким же id как у измененного элемиента по списку со всеми заметками                    break;
                }
                sch++;
            }
            if (du == (int)enumss.NeedToSave)
            {
                Notes[poz] = SortNote[ind];
            }
            else if (du == (int)enumss.NeedTpDelete)
            {
                Notes.RemoveAt(poz);
            }
            Serializator.mySer(Notes, file);
        }

    }
}
