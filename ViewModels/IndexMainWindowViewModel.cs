using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThisIsMyWar.ViewModels
{


    internal class IndexMainWindowViewModel : BindableBase
    {
        private string title="你好，世界！";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
