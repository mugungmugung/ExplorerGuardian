using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorerGuardian
{
    public class MainWindowViewModel : CommonNotifier
    {
        public RelayCommand? CertifiedButtonCommand { get; set; }
        public RelayCommand? CancelButtonCommand { get; set; }

        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set { SetField(ref _userName, value); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetField(ref _password, value); }
        }

        public MainWindowViewModel()
        {
            CertifiedButtonCommand = new RelayCommand(CertifiedButtonCommandExecute, () => true);
            CancelButtonCommand = new RelayCommand(CancelButtonCommandExecute, () => true);
        }

        private void CertifiedButtonCommandExecute(object parameter)
        {

        }

        private void CancelButtonCommandExecute(object parameter)
        {

        }
    }
}
