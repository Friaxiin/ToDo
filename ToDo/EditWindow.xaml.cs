using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDo.json");
        string Title;
        public EditWindow(Task task)
        {
            InitializeComponent();
            Title = task.Title;

            title.Text = task.Title;
            description.Text = task.Description;
            deadline.DisplayDate = task.Deadline.Date;
            priority.SelectedItem = task.Priority;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            string serializedTasks = File.ReadAllText(Path);
            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(serializedTasks);

            Task result = tasks.Find(t => t.Title == Title);

            result.Title = title.Text;
            result.Description = description.Text;
            result.Deadline = deadline.SelectedDate.Value;
            result.Priority = ((ComboBoxItem)priority.SelectedItem).Content.ToString();

            if (File.Exists(Path))
            {
                string serializedTasksWrite = JsonConvert.SerializeObject(tasks);
                File.WriteAllText(Path, serializedTasksWrite);
            }
            else
            {
                string serializedTasksWrite = JsonConvert.SerializeObject(new List<Task>());
                File.WriteAllText(Path, serializedTasksWrite);
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
