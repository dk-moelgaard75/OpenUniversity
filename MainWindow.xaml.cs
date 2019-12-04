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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenUniversity.Views;
using OpenUniversity.ViewModels;

namespace OpenUniversity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
        #region buttons
        private void BtnOpenEpmloyeeView(object sender, RoutedEventArgs e)
        {
            EmployeeView view = new EmployeeView();
            view.Closing += EmployeeView_Closing;
            view.ShowDialog();
        }

        private void BtnOpenStudentView(object sender, RoutedEventArgs e)
        {
            StudentView view = new StudentView();
            view.Closing += StudentView_Closing;
            view.ShowDialog();
        }
        private void BtnOpenCourseView(object sender, RoutedEventArgs e)
        {
            CourseView view = new CourseView();
            view.Closing += CourseView_Closing;
            view.ShowDialog();
        }
        private void BtnAddStudentToCourse(object sender, RoutedEventArgs e)
        {
            AddStudentToCourseView view = new AddStudentToCourseView();
            view.Closing += AddStudentToCourseView_Closing;
        }

        private void AddStudentToCourseView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewClosing();
        }

        private void CourseView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewClosing();
        }
        #endregion
        #region events
        private void StudentView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewClosing();
        }
        private void EmployeeView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewClosing();
        }
        #endregion

        #region private methods
        private void ViewClosing()
        {
            /*
            MainWindowViewModel tmpView = (MainWindowViewModel)this.DataContext;
            tmpView.SignalChanges();
            */
            ((MainWindowViewModel)this.DataContext).UpdateCollections();

        }
        #endregion
    }
}
