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

namespace TaskKeeper
{
    public partial class EditTaskWindow : Window
    {
        public string TaskTitle => txtTitle.Text.Trim();
        public DateTime? TaskDueDate
            => chkNoDue.IsChecked == true ? (DateTime?)null : dpDueDate.SelectedDate;
        public EditTaskWindow(TaskItem item)
        {
            InitializeComponent();
            txtTitle.Text = item.Title;
            if (item.DueDate.HasValue)
            {
                dpDueDate.SelectedDate = item.DueDate.Value;
                chkNoDue.IsChecked = false;
            }
            else
            {
                dpDueDate.SelectedDate = DateTime.Today;
                chkNoDue.IsChecked = true;
                dpDueDate.IsEnabled = false;
            }

            chkNoDue.Checked += (_, __) => dpDueDate.IsEnabled = false;
            chkNoDue.Unchecked += (_, __) => dpDueDate.IsEnabled = true;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitle))
            {
                MessageBox.Show("Текст задачи не может быть пустым.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}
