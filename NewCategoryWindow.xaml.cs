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
    /// Логика взаимодействия для NewCategoryWindow.xaml
    /// </summary>
    public partial class NewCategoryWindow : Window
    {
        public NewCategoryWindow()
        {
            InitializeComponent();
            NewCategoryName.Text = null;
        }

        private void SaveNewCategory(object sender, RoutedEventArgs e)
        {
            Category c = new Category();
            c.Name = NewCategoryName.Text;
            GlobalContext.db.Categories.Add(c);
            AddNoteWindow addnoteform = this.Owner as AddNoteWindow;
            var cnt = GlobalContext.db.Categories.Count();
            List<Category> cat = GlobalContext.db.Categories.ToList<Category>();
            string category = cat.Last().Name;
            if (addnoteform != null)
            {
                addnoteform.CategoryComboBox.Items.Add(category);
            }
            Close();
        }
    }
}
