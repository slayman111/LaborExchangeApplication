using LaborExchangeApplication.Command;
using LaborExchangeApplication.Model;
using LaborExchangeApplication.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaborExchangeApplication.ViewModel
{
    public class InformationBoxViewModel : MainViewModel
    {
        #region Fields

        private string _title;
        private string _information;
        private BitmapImage _image;

        #endregion Fields

        #region Properties

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Information { get => _information; set => SetProperty(ref _information, value); }
        public BitmapImage Image { get => _image; set => SetProperty(ref _image, value); }

        public ICommand CloseWindowCommand { get; private set; }

        #endregion Properties

        #region Methods

        public InformationBoxViewModel(string title, string information, InformationBoxImage image)
        {
            CloseWindowCommand = new DelegateCommand(CloseWindow);

            (Title, Information) = (title, information);
            switch (image)
            {
                case InformationBoxImage.Information: Image = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/information.png")); break;
                case InformationBoxImage.Warning: Image = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/warning.png")); break;
                case InformationBoxImage.Error: Image = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/error.png")); break;
            }
        }

        private void CloseWindow(object obj) =>
            Application.Current?.Windows.Cast<Window>()?.FirstOrDefault(window => window is InformationBoxWindow)?.Close();

        #endregion Methods
    }
}