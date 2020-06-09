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
    /// Логика взаимодействия для DeleteForm.xaml
    /// </summary>
    public partial class DeleteForm : Window
    {
        private Note delItem;
        public DeleteForm(Note delItem)
        {
            this.delItem = delItem;
            InitializeComponent();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MainWindow m = Owner as MainWindow;
            GlobalContext.NoteList.Remove(delItem);
            AddNoteWindow addNote = new AddNoteWindow();
            addNote.RefreshNotes(m);
            Close();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
