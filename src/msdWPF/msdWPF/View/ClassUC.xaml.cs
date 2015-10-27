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
    /// Interaction logic for ClassUC.xaml
    /// </summary>
    public partial class ClassUC : UserControl
    {
        private ClassViewModel _viewModel;

        public ClassUC()
        {
            InitializeComponent();

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                _viewModel = FindResource("classViewModel") as ClassViewModel;
                _viewModel.SetClassModel(new ClassModel());
            }
        }

        private void OnChangedSelectedClassType(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.NotifyPropertyChanged("SchoolClassList");
            _viewModel.NotifyPropertyChanged("CanSelecteCurrentClass");
            _viewModel.NotifyPropertyChanged("CanAddNewClass");
        }

        private void OnChangeSelectedSemester(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.NotifyPropertyChanged("SchoolClassList");
            _viewModel.NotifyPropertyChanged("CanSelecteCurrentClass");
            _viewModel.NotifyPropertyChanged("CanAddNewClass");
        }

        private void ClearClass_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearSelectSchoolClass();
            SchoolClassComboBox.SelectedIndex = -1;
        }

        private void CurrentSelectedClassChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.CurrentSelectedClass = (SchoolClass)SchoolClassComboBox.SelectedItem;
        }

        private void AddClass_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddNewClass();
        }

        private void ClassInformationEdit_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.EditCurrentClassInformationClick();
        }

        private void ClassInformationSave_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void NonClassDateEdit_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void NonClassDateEditCancel_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void NonClassDateEditSave_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void NonClassDateEditDelete_onClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
