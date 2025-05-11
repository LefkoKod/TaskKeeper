using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Linq;

namespace TaskKeeper
{
    public partial class MainWindow : Window
    {
        private readonly TaskManager _manager;

        public MainWindow()
        {
            InitializeComponent();
            _manager = new TaskManager();
            RefreshList();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var text = txtNew.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Введите текст задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _manager.Add(text);
            txtNew.Clear();
            RefreshList();
        }
        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem ti)
            {
                _manager.Toggle(ti.Id);
                RefreshList();
            }
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem ti)
            {
                var res = MessageBox.Show($"Удалить задачу «{ti.Title}»?", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    _manager.Remove(ti.Id);
                    RefreshList();
                }
            }
        }
        private void lstTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem item)
            {
                var dlg = new EditTaskWindow(item) { Owner = this };
                if (dlg.ShowDialog() == true)
                {
                    item.Title = dlg.TaskTitle;
                    item.DueDate = dlg.TaskDueDate;
                    _manager.Update(item);
                    RefreshList();
                }
            }
        }
        private void RefreshList()
        {
            lstTasks.ItemsSource = null;
            lstTasks.ItemsSource = _manager.Tasks.OrderBy(t => t.CreatedAt).ToList();
        }
    }
}