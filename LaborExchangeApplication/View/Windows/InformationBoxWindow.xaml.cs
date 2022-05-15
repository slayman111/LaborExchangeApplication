using LaborExchangeApplication.Model;
using LaborExchangeApplication.ViewModel;
using System;
using System.Collections.Generic;
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

namespace LaborExchangeApplication.View.Windows
{
    /// <summary>
    /// Interaction logic for InformationBoxWindow.xaml
    /// </summary>
    public partial class InformationBoxWindow : Window
    {
        public InformationBoxWindow(string title, string information, InformationBoxImage image)
        {
            InitializeComponent();

            DataContext = new InformationBoxViewModel(title, information, image);
        }
    }
}