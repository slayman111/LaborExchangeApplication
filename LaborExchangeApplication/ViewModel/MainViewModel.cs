using LaborExchangeApplication.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LaborExchangeApplication.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand SwitchColorModeCommand { get; private set; }

        static MainViewModel() => Properties.ColorSettings.Default.ColorMode = (DateTime.Now.Hour >= 17 || DateTime.Now.Hour <= 4) ? "Dark" : "Light";

        public MainViewModel() => SwitchColorModeCommand = new DelegateCommand(SwitchColorMode);

        private void SwitchColorMode(object obj) =>
            Properties.ColorSettings.Default.ColorMode = Properties.ColorSettings.Default.ColorMode == "Light" ? "Dark" : "Light";
    }
}