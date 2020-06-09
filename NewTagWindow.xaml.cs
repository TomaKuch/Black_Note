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
    /// Логика взаимодействия для NewTagWindow.xaml
    /// </summary>
    public partial class NewTagWindow : Window
    {
        public NewTagWindow()
        {
            InitializeComponent();
        }

        private void SaveNewTag(object sender, RoutedEventArgs e)
        {
            Tag t = new Tag();
            t.Name = NewTagName.Text;
            GlobalContext.db.Tags.Add(t);
            AddNoteWindow addnoteform = this.Owner as AddNoteWindow;
            List<Tag> tags = GlobalContext.db.Tags.ToList();
            string tag = tags.Last().Name;
            if (addnoteform != null)
            {
                addnoteform.TagComboBox.Items.Add(tag);
            }
            Close();
        }
    }
}
