using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Black_Note
{
    /// <summary>
    /// Логика взаимодействия для EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        private Note itemToShow;
        public EditNoteWindow(Note itemToShow)
        {
            this.itemToShow = itemToShow;
            InitializeComponent();

            foreach (var v in GlobalContext.db.priorityLevels)
            {
                PriorityComboBox_1.Items.Add(v.Name);
                if (v.Name.Equals(itemToShow.PriorityLevel.Name))
                {
                    PriorityComboBox_1.SelectedIndex = PriorityComboBox_1.Items.IndexOf(v.Name);
                }
            }
            
            foreach (var v in GlobalContext.db.Categories)
            {
                CategoryComboBox_1.Items.Add(v.Name);
                if (v.Name.Equals(itemToShow.Category.Name))
                {
                    CategoryComboBox_1.SelectedItem = v.Name;
                }
            }

            foreach (var v in GlobalContext.db.Tags)
            {
                TagComboBox_1.Items.Add(v.Name);
                if (v.Id == itemToShow.TagId)
                {
                    TagComboBox_1.SelectedItem = v.Name;
                }
            }

            NewNote_1.Text = itemToShow.Description;
            DeadlineDate_1.SelectedDate = itemToShow.Deadline;
            
        }

        private void EditNote(object sender, RoutedEventArgs e)
        {
            Note n = new Note();
            n.State = itemToShow.State;
            n.Description = NewNote_1.Text;
            n.Deadline = Convert.ToDateTime(DeadlineDate_1.Text);
            var myLvl = GlobalContext.db.priorityLevels
                .Where(lvl => lvl.Name == PriorityComboBox_1.SelectedItem.ToString()).First();
            n.PriorityLevel = myLvl;

            var category = GlobalContext.db.Categories
                .Where(c => c.Name == CategoryComboBox_1.SelectedItem.ToString()).First();
            n.Category = category;

            var tag = GlobalContext.db.Tags
                .Where(t => t.Name == TagComboBox_1.SelectedItem.ToString()).First();
            if (tag.Name == "None")
            {
                n.TagId = null;
            }
            else
            {
                n.TagId = tag.Id;
            }


            MainWindow main = this.Owner as MainWindow;
            int indexOfItem = GlobalContext.NoteList.IndexOf(itemToShow);
            GlobalContext.NoteList.Remove(itemToShow);
            GlobalContext.NoteList.Insert(indexOfItem, n);
            AddNoteWindow w = new AddNoteWindow();
            w.RefreshNotes(main);
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
