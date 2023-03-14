using CheckRequestWorker.Tools;
using CheckRequestWorker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CheckRequestWorker.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private Page currentPage;

        public MainWindowVM()
        {
            CurrentPage = new SignInPage(this);
        }

        public Page CurrentPage 
        { 
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }
    }
}
