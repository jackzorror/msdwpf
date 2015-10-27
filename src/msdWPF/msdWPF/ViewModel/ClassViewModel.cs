using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msdWPF.Model;

namespace msdWPF.ViewModel
{
    public class ClassViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// PropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        private ClassModel _classModel;

        #region ClassUC


        internal void SetClassModel(ClassModel classModel)
        {
            _classModel = classModel;
            _semesterList = _classModel.FindAllSemesterList();
            NotifyPropertyChanged("");
        }

        public List<SchoolSemester> SemesterListForClass
        {
            get
            {
                if (null == _semesterList && null != _classModel)
                {
                    _semesterList = _classModel.FindAllSemesterList();
                }
                return _semesterList;
            }
        }

        private List<SchoolSemester> _semesterList;

        public List<ApplicationType> ClassTypeList
        {
            get
            {
                if (null == _classTypeList && null != _classModel)
                {
                    _classTypeList = _classModel.FindClassTypeList();
                }
                return _classTypeList;
            }
        }

        private List<ApplicationType> _classTypeList; 

        public String ClassOpertionErrorMesage { get; set; }


        #endregion

    }
}
