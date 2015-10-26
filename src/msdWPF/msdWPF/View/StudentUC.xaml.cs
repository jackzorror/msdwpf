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
using msdWPF.Model;
using msdWPF.ViewModel;

namespace msdWPF.View
{
    /// <summary>
    /// Interaction logic for StudentUC.xaml
    /// </summary>
    public partial class StudentUC : UserControl
    {
        private StudentViewModel _viewModel;

        public StudentUC()
        {
            InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _viewModel = FindResource("studentViewModel") as StudentViewModel;
                _viewModel.SetStudentModel(new StudentModel());
                this.StudentInformationUc.SetStudentViewModel(_viewModel);
            }
        }

        private void StudentSearch_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchStudentByFirstLastName();
        }

        private void StudnetCancel_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelSearchStudent();
        }

        private void StudentLastNameSearch_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.SearchStudentByFirstLastName();
            }
        }

        private void StudnetAdd_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddNewStudent();
        }
    }
}
