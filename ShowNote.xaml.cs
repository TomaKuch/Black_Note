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
    /// Логика взаимодействия для ShowNote.xaml
    /// </summary>
    public partial class ShowNote : Window
    {
        public ShowNote(Note itemToShow)
        {
            InitializeComponent();
            NoteText.Content = itemToShow.Description;
            Deadline.Content = itemToShow.Deadline.ToString();
            Priority.Content = itemToShow.PriorityLevel.Name;
            Category.Content = itemToShow.Category.Name;
            var tag = from t in GlobalContext.db.Tags
                      where t.Id == itemToShow.TagId
                      select t.Name;
            Tag.Content = tag.First();
        }

        private void CloseNote(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
