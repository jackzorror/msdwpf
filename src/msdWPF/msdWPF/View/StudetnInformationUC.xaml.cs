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
    /// Interaction logic for StudetnInformationUC.xaml
    /// </summary>
    public partial class StudetnInformationUC : UserControl
    {
        private StudentViewModel _viewModel;

        public StudetnInformationUC()
        {
            InitializeComponent();
        }

        public void SetStudentViewModel(StudentViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        private void StudentInformationEdit_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.EditStudentInformation();
        }

        private void StudnetInformationSave_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveStudentInformation();
        }

        private void MedicalInformationEdit_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.EditStudentMedical();
        }

        private void MedicalInformationSave_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveStudentMedical();
        }

        private void StudentLastName_onChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.StudentLastNameChanged = true;
        }

        private void StudentFirstName_onChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.StudentFirstNameChanged = true;
        }

    }
}
