using CheckRequestWorker.DB;
using CheckRequestWorker.Models;
using CheckRequestWorker.Tools;
using CheckRequestWorker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckRequestWorker.ViewModels
{
    public class SignInPageVM: BaseVM
    {
        public int Code { get; set; }
        public Command SignIn { get; set; }

        public SignInPageVM(MainWindowVM mainVM)
        {

            SignIn = new Command(() =>
            {
                Worker worker = null;
                try
                {
                    worker = user50_2Context.GetInstance().Workers.FirstOrDefault(s => s.Code == Code);
                }
                catch
                {
                    MessageBox.Show("Не удалось подключиться к БД");
                    return;
                }
                if(worker == null || worker.DepartmentId != 1)
                {
                    MessageBox.Show("В доступе отказано");
                }
                else
                {
                    mainVM.CurrentPage = new ListRequestPage();
                }
            });
        }
    }
}
