using ArmEquipmentRepair.Application.Contracts.Services;
using ArmEquipmentRepair.Domain.Exceptions;
using ArmEquipmentRepair.UI.Common;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ArmEquipmentRepair.UI.Services
{
    public class TaskManager
    {
        private static Task _currentTask;
        private static Queue<Task> _queue = new Queue<Task>();
        private static LoadingWindow _loadingWindow = new LoadingWindow();

        private static event EventHandler<Task> _taskCreated;

        private static Dictionary<Exception, string> _exceptionsMessages = new Dictionary<Exception, string>
        {
            {new LoginExistException(), "Введённый логин уже существует. Попробуйте ввести другой" },
            {new LoginNotExistException(), "Введенный логин не существует" },
            {new AuthorizeException(), "Неверный пароль" },
        };


        public static void WaitTaskWithLoadingWindow(Action action)
        {

            _loadingWindow.Show();

            var task = Task.Run(async () =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    var key = _exceptionsMessages.Keys.FirstOrDefault(k => k.GetType() == ex.GetType());

                    if (key != default)
                    {
                        MessageBox.Show(_exceptionsMessages[key], "", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Ошибка", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });


            Task.WaitAll(task);
            _loadingWindow.Close();
        }


        static TaskManager()
        {
            _taskCreated += TaskManager_taskCreatedAsync;
        }


        private static async void TaskManager_taskCreatedAsync(object? sender, Task e)
        {
            while (_queue.Count > 0)
            {
                if (_currentTask == null)
                    await StartTask();
            }
        }

        private static async Task StartTask()
        {
            _currentTask = _queue.Dequeue();
            _currentTask.Start();
            await _currentTask;
            _currentTask = null;
        }

        public static void AddTask(Action action)
        {
            Task task = new Task(action);
            _queue.Enqueue(task);
            if (_currentTask == null)
            {
                _taskCreated?.Invoke(null, task);
            }
        }

        public static void WaitAllTask()
        {
            while (_queue.Count > 0)
            {
            }
        }
    }
}
