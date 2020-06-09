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
    /// Логика взаимодействия для AddNoteWindow.xaml
    /// </summary>
    public partial class AddNoteWindow : Window
    {
        public AddNoteWindow()
        {
            InitializeComponent();
            LoadInfoFromDBToComboBoxes();
        }

        public void LoadInfoFromDBToComboBoxes()
        {
                foreach (var v in GlobalContext.db.priorityLevels)
                {
                    PriorityComboBox.Items.Add(v.Name);
                }

                foreach (var v in GlobalContext.db.Categories)
                {
                    CategoryComboBox.Items.Add(v.Name);
                }

                foreach (var v in GlobalContext.db.Tags)
                {
                    TagComboBox.Items.Add(v.Name);
                }

            TagComboBox.SelectedItem = 1;
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryWindow newCategory = new NewCategoryWindow();
            newCategory.Owner = this;
            newCategory.Show();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            NewTagWindow tagWindow = new NewTagWindow();
            tagWindow.Owner = this;
            tagWindow.Show();
        }

        private void SaveNote(object sender, RoutedEventArgs e)
        {
            Note n = new Note();
            n.State = false;
            n.Description = NewNote.Text;
            n.Deadline = Convert.ToDateTime(DeadlineDate.Text);
            var myLvl = GlobalContext.db.priorityLevels
                .Where(lvl => lvl.Name == PriorityComboBox.SelectedItem.ToString()).First();
                n.PriorityLevel = myLvl;

            var category = GlobalContext.db.Categories
                .Where(c => c.Name == CategoryComboBox.SelectedItem.ToString()).First();
                n.Category = category;

            var tag = GlobalContext.db.Tags
                .Where(t => t.Name == TagComboBox.SelectedItem.ToString()).First();
            if(tag.Name == "None")
            {
                n.TagId = null;
            }
            else
            {
                n.TagId = tag.Id;
            }
                
            
            MainWindow main = this.Owner as MainWindow;
            GlobalContext.NoteList.Add(n);
            RefreshNotes(main);
            Close();
        }

        public void RefreshNotes(MainWindow main)
        {
            if (GlobalContext.cnt == 0)
            {
                //GlobalContext.NoteList = GlobalContext.db.Notes.AsEnumerable().ToList();
                GlobalContext.NoteList.Capacity = GlobalContext.db.Notes.ToList().Capacity;

                foreach( Note n in GlobalContext.db.Notes)
                    GlobalContext.NoteList.Add(n);
                GlobalContext.cnt++;
            }

            main.NoteListBox.Items.Clear();

            List<Note> notesWithHighPriority = new List<Note>();
            foreach(Note n in GlobalContext.NoteList)
            {
                if(n.PriorityLevel.Name.Equals("High"))
                {
                    notesWithHighPriority.Add(n);
                }
            }

            List<Note> notesWithMediumPriority = new List<Note>();
            foreach (Note n in GlobalContext.NoteList)
            {
                if (n.PriorityLevel.Name.Equals("Medium"))
                {
                    notesWithMediumPriority.Add(n);
                }
            }

            List<Note> notesWithLowPriority = new List<Note>();
            foreach (Note n in GlobalContext.NoteList)
            {
                if (n.PriorityLevel.Name.Equals("Low"))
                {
                    notesWithLowPriority.Add(n);
                }
            }

            //var notesWithHighPriority = from n in GlobalContext.NoteList
            //                            where n.PriorityLevel.Name == "High"
            //                            orderby n.State
            //                            select n;

            //var notesWithMediumPriority = from n in GlobalContext.NoteList
            //                              where n.PriorityLevel.Name == "Medium"
            //                              orderby n.State
            //                              select n;

            //var notesWithLowPriority = from n in GlobalContext.NoteList
            //                           where n.PriorityLevel.Name == "Low"
            //                           orderby n.State
            //                           select n;

            List<Note> newList = new List<Note>();

            newList.AddRange(notesWithHighPriority);
            newList.AddRange(notesWithMediumPriority);
            newList.AddRange(notesWithLowPriority);

            GlobalContext.NoteList.Clear();
            GlobalContext.NoteList.AddRange(newList);
          
            int index = 0;
            foreach (Note n in GlobalContext.NoteList)
            {
                AddNoteToList(index, n, main);
                index++;
            }

        }



        public void AddNoteToList(int index, Note note, MainWindow main)
        {
            if (main != null)
            {
                ListBoxItem item = new ListBoxItem();
                item.Tag = index;
                StackPanel panel = new StackPanel();
                CheckBox stateCheckBox = new CheckBox();
                TextBlock noteText = new TextBlock();
                Button showBut = new Button();
                Button editBut = new Button();
                Button delBut = new Button();

                panel.Orientation = Orientation.Horizontal;
                panel.Height = 39;
                panel.Tag = index;

                stateCheckBox.Margin = new Thickness(1,1,1,1);
                stateCheckBox.BorderBrush = Brushes.Black;
                stateCheckBox.BorderThickness = new Thickness(2, 2, 2, 2);
                stateCheckBox.FontSize = 29;
                stateCheckBox.IsChecked = note.State;
                stateCheckBox.MinHeight = 20;
                stateCheckBox.MinWidth = 20;
                stateCheckBox.Tag = index;
                stateCheckBox.Checked += main.ChangeStateOfNote;
                stateCheckBox.Unchecked += main.ChangeStateOfNote;

                noteText.Width = 492;
                noteText.Margin = new Thickness(1, 1, 1, 1);
                noteText.Height = 29;
                noteText.Text = note.Description;
                noteText.Tag = index;

                showBut.Width = 85;
                showBut.Tag = index;
                showBut.Content = "Show";
                showBut.Click += main.ShowItem;
                editBut.Width = 85;
                editBut.Tag = index;
                editBut.Content = "Edit";
                editBut.Click += main.EditItem;
                delBut.Width = 85;
                delBut.Tag = index;
                delBut.Content = "Delete";
                delBut.Click += main.DeleteItem;

                panel.Children.Add(stateCheckBox);
                panel.Children.Add(noteText);
                panel.Children.Add(showBut);
                panel.Children.Add(editBut);
                panel.Children.Add(delBut);

                item.Content = panel;
                main.NoteListBox.Items.Add(item);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
