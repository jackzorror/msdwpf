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

    }
}
