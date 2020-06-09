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

namespace Black_Note
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///
    static class GlobalContext
    {
        public static NoteContext db = new NoteContext();
        public static List<Note> NoteList = new List<Note>();
        public static int cnt = 0;
    }

    public partial class MainWindow : Window
    {
        AddNoteWindow addNote;
        public MainWindow()
        {
            InitializeComponent();

            //Category c1 = new Category { Name = "Work" };
            //Category c2 = new Category { Name = "Home" };
            //Category c3 = new Category { Name = "Study" };
            //PriorityLevel l1 = new PriorityLevel { Name = "Low" };
            //PriorityLevel l2 = new PriorityLevel { Name = "Medium" };
            //PriorityLevel l3 = new PriorityLevel { Name = "High" };
            //Tag t4 = new Tag { Name = "None" };
            //Tag t1 = new Tag { Name = "To do as fast as possible" };
            //Tag t2 = new Tag { Name = "Task for vacation" };
            //Tag t3 = new Tag { Name = "self-development" };

            //GlobalContext.db.Categories.AddRange(new List<Category> { c1, c2, c3 });
            //GlobalContext.db.priorityLevels.AddRange(new List<PriorityLevel> { l1, l2, l3 });
            //GlobalContext.db.Tags.AddRange(new List<Tag> { t4, t1, t2, t3 });
            //GlobalContext.db.SaveChanges();

            addNote = new AddNoteWindow();
            addNote.RefreshNotes(this);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            addNote = new AddNoteWindow();
            addNote.Owner = this;
            addNote.Show();
        }

        public void ChangeStateOfNote(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (box != null)
            {
                GlobalContext.NoteList[(int)box.Tag].State = box.IsChecked.Value;
            }

        }

        public void EditItem(object sender, RoutedEventArgs e)
        {
            Button editBut = sender as Button;
            if (editBut != null)
            {
                Note itemToShow = GlobalContext.NoteList[(int)editBut.Tag];
                EditNoteWindow f = new EditNoteWindow(itemToShow);
                f.Owner = this;
                f.Show();
            }
        }

        public void ShowItem(object sender, RoutedEventArgs e)
        {
            Button showBut = sender as Button;
            if (showBut != null)
            {
                Note itemToShow = GlobalContext.NoteList[(int)showBut.Tag];
                ShowNote f = new ShowNote(itemToShow);
                f.Owner = this;
                f.Show();
            }
        }
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            Button delBut = sender as Button;
            if(delBut != null)
            {
                Note itemToDel = GlobalContext.NoteList[(int)delBut.Tag];
                DeleteForm f = new DeleteForm(itemToDel);
                f.Owner = this;
                f.Show();
            }
            
        }

        private void SaveInDB(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlobalContext.db.Notes.RemoveRange(GlobalContext.db.Notes);
            GlobalContext.db.SaveChanges();
            GlobalContext.db.Notes.AddRange(GlobalContext.NoteList);
            GlobalContext.db.SaveChanges();
        }

        private void SortByPriority(object sender, RoutedEventArgs e)
        {
            NoteListBox.Items.Clear();
            var notesWithHighPriority = from n in GlobalContext.NoteList
                                        where n.PriorityLevel.Name == "High"
                                        select n;

            var notesWithMediumPriority = from n in GlobalContext.NoteList
                                          where n.PriorityLevel.Name == "Medium"
                                          select n;

            var notesWithLowPriority = from n in GlobalContext.NoteList
                                       where n.PriorityLevel.Name == "Low"
                                       select n;

            List<Note> newList = new List<Note>();
            newList.AddRange(notesWithHighPriority);
            newList.AddRange(notesWithMediumPriority);
            newList.AddRange(notesWithLowPriority);
            GlobalContext.NoteList.Clear();
            GlobalContext.NoteList.AddRange(newList);
            RefreshList();


        }

        private void SortByTag(object sender, RoutedEventArgs e)
        {
            
            var tagList = GlobalContext.db.Tags.ToList<Tag>();
            List<Note> newList = new List<Note>();

            for (int i = 0; i < tagList.Count; i++)
            {
                var listWithOneTag = from n in GlobalContext.NoteList
                                     where n.TagId == tagList[i].Id
                                     select n;
                newList.AddRange(listWithOneTag);
            }

            GlobalContext.NoteList.Clear();
            GlobalContext.NoteList.AddRange(newList);
            NoteListBox.Items.Clear();
            RefreshList();
        }


        private void RefreshList()
        {
            int index = 0;
            foreach (Note n in GlobalContext.NoteList)
            {
                addNote.AddNoteToList(index, n, this);
                index++;
            }
        }

        private void SortByCategory(object sender, RoutedEventArgs e)
        {
            NoteListBox.Items.Clear();
            var categoriesList = GlobalContext.db.Categories.ToList();
            List<Note> newList = new List<Note>();

            for (int i = 0; i < categoriesList.Count; i++)
            {
                var listWithOneCatedgry = from n in GlobalContext.NoteList
                                     where n.Category.Name == categoriesList[i].Name
                                     select n;
                newList.AddRange(listWithOneCatedgry);
            }

            GlobalContext.NoteList.Clear();
            GlobalContext.NoteList.AddRange(newList);
            NoteListBox.Items.Clear();
            RefreshList();
        }


    }
}
