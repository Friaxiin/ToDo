using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDo.json");
        public List<Task> Tasks = new List<Task>();
        public MainWindow()
        {
            InitializeComponent();
            
            ReadTasks();

        }
        private void ReadTasks()
        {
            string serializedTasks = File.ReadAllText(Path);
            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(serializedTasks);
            Tasks = tasks;

            TaskList.ItemsSource = Tasks;
        }

        private void AddTask(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.Title = title.Text;
            task.Description = description.Text;
            task.Deadline = deadline.SelectedDate.Value;
            task.Priority = ((ComboBoxItem)priority.SelectedItem).Content.ToString();
            task.IsCompleted = "NIEZAKOŃCZONE";

            Tasks.Add(task);


            if (File.Exists(Path))
            {
                string serializedTasks = JsonConvert.SerializeObject(Tasks);
                File.WriteAllText(Path, serializedTasks);
                ReadTasks();
            }
            else
            {
                string serializedTasks = JsonConvert.SerializeObject(new List<Task>());
                File.WriteAllText(Path, serializedTasks);
                ReadTasks();
            }
        }

        private void EditTask(object sender, RoutedEventArgs e)
        {
            Task task = (Task)TaskList.SelectedItem;

            if (task != null)
            {
                EditWindow editWindow = new EditWindow(task);
                editWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wybierz element z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            Task task = (Task)TaskList.SelectedItem;

            if (task != null)
            {
                string title = task.Title;
                List<Task> tasks = Tasks;

                Task result = tasks.Find(t => t.Title == title);

                tasks.Remove(result);

                if (File.Exists(Path))
                {
                    string serializedTasks = JsonConvert.SerializeObject(Tasks);
                    File.WriteAllText(Path, serializedTasks);
                    ReadTasks();
                }
                else
                {
                    string serializedTasks = JsonConvert.SerializeObject(new List<Task>());
                    File.WriteAllText(Path, serializedTasks);
                    ReadTasks();
                }
            }
            else
            {
                MessageBox.Show("Wybierz element z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FinishTask(object sender, RoutedEventArgs e)
        {
            Task task = (Task)TaskList.SelectedItem;

            if (task != null)
            {
                string title = task.Title;
                List<Task> tasks = Tasks;

                Task result = tasks.Find(t => t.Title == title);

                result.IsCompleted = "UKOŃCZONE";

                if (File.Exists(Path))
                {
                    string serializedTasks = JsonConvert.SerializeObject(Tasks);
                    File.WriteAllText(Path, serializedTasks);
                    ReadTasks();
                }
                else
                {
                    string serializedTasks = JsonConvert.SerializeObject(new List<Task>());
                    File.WriteAllText(Path, serializedTasks);
                    ReadTasks();
                }
            }
            else
            {
                MessageBox.Show("Wybierz element z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}