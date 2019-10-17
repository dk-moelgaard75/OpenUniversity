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
using OpenUniversity.ViewModels;


namespace OpenUniversity.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView()
        {
            InitializeComponent();
            DataContext = new EmployeeViewModel();
        }
        private EmployeeViewModel CurrentViewModel => DataContext as EmployeeViewModel;
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (CurrentViewModel == null) return;
            CurrentViewModel.HandleKeyDown(e);

        }
    }
}
