using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            _viewModel.NotifyPropertyChanged("SchoolClassSummaryList");
            _viewModel.NotifyPropertyChanged("CanSelecteCurrentClass");
            _viewModel.NotifyPropertyChanged("CanAddNewClass");
        }

        private void OnChangeSelectedSemester(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.NotifyPropertyChanged("SchoolClassSummaryList");
            _viewModel.NotifyPropertyChanged("CanSelecteCurrentClass");
            _viewModel.NotifyPropertyChanged("CanAddNewClass");
        }

        private void ClearClass_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearSelectSchoolClass();
            SchoolClassSummaryComboBox.SelectedIndex = -1;
        }

        private void CurrentSelectedClassChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedSchoolClassSummary = (SchoolClassSummary)SchoolClassSummaryComboBox.SelectedItem;
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
            _viewModel.SaveCurrentSchoolClass();

            SchoolClassSummaryComboBox.SelectedItem = _viewModel.SelectedSchoolClassSummary;
            //((SchoolClassSummary)SchoolClassSummaryComboBox.SelectedItem).Name = _viewModel.CurrentSelectedClass.Name;
        }

        private void NonClassDateEdit_onClick(object sender, RoutedEventArgs e)
        {
            NonClassDateEditPopup.IsOpen = true;
            _viewModel.EditNonClassDate();
            ICollectionView view = CollectionViewSource.GetDefaultView(NonClassDateListView.ItemsSource);
            if (null != view)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("NonClassDateTime", ListSortDirection.Ascending));
                view.Refresh();
            }
        }

        private void NonClassDateEditCancel_onClick(object sender, RoutedEventArgs e)
        {
            NonClassDateEditPopup.IsOpen = false;
            _viewModel.CancelEditNonClassDate();
        }

        private void NonClassDateEditSave_onClick(object sender, RoutedEventArgs e)
        {
            NonClassDateEditPopup.IsOpen = false;
            _viewModel.SaveEditNonClassDate();
        }

        private void NonClassDateEditDelete_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.DeletSelectedNonClassDate();
            /*
            ICollectionView view = CollectionViewSource.GetDefaultView(NonClassDateListView.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("NonClassDateTime", ListSortDirection.Ascending));
            view.Refresh();
            */
        }

        private void SearchClass_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.SearchSchoolClass();
        }

        private void NonClassDateEditAdd_onClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddNonClassDate();
            ICollectionView view = CollectionViewSource.GetDefaultView(NonClassDateListView.ItemsSource);
            if (null != view)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("NonClassDateTime", ListSortDirection.Ascending));
                view.Refresh();
            }
        }

        private void ClassSchedulerEdit_onClick(object sender, RoutedEventArgs e)
        {
            ClassSchedulerEditPopup.IsOpen = true;
            _viewModel.EditClassScheduler();
        }

        private void ClassSchedulerEditAdd_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void ClassSchedulerEditDelete_onClick(object sender, RoutedEventArgs e)
        {

        }

        private void ClassSchedulerEditCancel_onClick(object sender, RoutedEventArgs e)
        {
            ClassSchedulerEditPopup.IsOpen = false;
        }

        private void ClassSchedulerEditSave_onClick(object sender, RoutedEventArgs e)
        {
            ClassSchedulerEditPopup.IsOpen = false;
        }

    }
}
