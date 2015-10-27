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

        private List<SchoolClass> _allSchoolClasses;

        internal void SetClassModel(ClassModel classModel)
        {
            _classModel = classModel;

            _semesterList = _classModel.FindAllSemesterList();
            _allSchoolClasses = _classModel.FindAllSchoolClass();

            ClassOpertionErrorMesage = "";
            ShowClassInformationPanel = "Hidden";
            ClassInformationEditButtonLabel = "Eidt";
            CanEditClassIfnormation = false;

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

        public SchoolSemester SelectedSchoolSemester { get; set; }

        public ApplicationType SelectedClassType { get; set; }

        public bool CanSelecteCurrentClass { get { return (null != SelectedClassType && null != SelectedSchoolSemester); } }
        public bool CanAddNewClass { get { return (null != SelectedClassType && null != SelectedSchoolSemester); } }

        public List<SchoolClass> SchoolClassList
        {
            get
            {
                List<SchoolClass> classes = null;
                if (null != SelectedSchoolSemester && null != SelectedClassType)
                {
                    classes = _allSchoolClasses.FindAll(x => x.SemesterId == SelectedSchoolSemester.Id).FindAll(x => x.ClassTypeId == SelectedClassType.Id);
                } 
                else if (null != SelectedSchoolSemester)
                {
                    classes = _allSchoolClasses.FindAll(x => x.SemesterId == SelectedSchoolSemester.Id);
                }
                else if (null != SelectedClassType)
                {
                    classes = _allSchoolClasses.FindAll(x => x.ClassTypeId == SelectedClassType.Id);
                }
                else
                {
                    classes = _allSchoolClasses;
                }

                return classes;
            }
        }

        internal void AddNewClass()
        {
            if (null == SelectedSchoolSemester)
            {
                ClassOpertionErrorMesage = "Please select Semester from Semester List";
                NotifyPropertyChanged("");
                return;
            } 
            else if (null == SelectedClassType)
            {
                ClassOpertionErrorMesage = "Please select Class Type from Class Type List";
                NotifyPropertyChanged("");
                return;
            }
            ClassOpertionErrorMesage = "";
            CurrentSelectedClass = new SchoolClass();
            
        }


        internal void ClearSelectSchoolClass()
        {
            SelectedClassType = null;
            SelectedSchoolSemester = null;
            _currentSelectedClass = null;

            ClassOpertionErrorMesage = "";
            ShowClassInformationPanel = "Hidden";
            ClassInformationEditButtonLabel = "Edit";
            CanEditClassIfnormation = false;
            CanClickClassInformationSave = false;

            NotifyPropertyChanged("");
        }

        #endregion

        #region Class Information

        public String ClassInformationEditButtonLabel { get; set; }
        public bool CanClickClassInformationSave { get; set; }
        public String ShowClassInformationPanel { get; set; }
        public bool CanEditClassIfnormation { get; set; }
        public String NonClassDateList { get; set; }


        public SchoolClass CurrentSelectedClass
        {
            get
            {
                return _currentSelectedClass;
            }
            set
            {
                _currentSelectedClass = value;

                if (null != _currentSelectedClass)
                {
                    ClassOpertionErrorMesage = "";
                    ShowClassInformationPanel = "Visible";
                    ClassInformationEditButtonLabel = "Edit";
                    CanEditClassIfnormation = false;
                }
                else
                {
                    ClassOpertionErrorMesage = "";
                    ShowClassInformationPanel = "Hidden";
//                    ClassInformationEditButtonLabel = "Edit";
//                    CanEditClassIfnormation = false;
                }
                NotifyPropertyChanged("");
            }
        }

        private SchoolClass _currentSelectedClass;

        internal void EditCurrentClassInformationClick()
        {
            if (ClassInformationEditButtonLabel.Equals("Edit"))
            {
                ClassInformationEditButtonLabel = "Cancel";
                CanClickClassInformationSave = true;
                CanEditClassIfnormation = true;
            } 
            else if (ClassInformationEditButtonLabel.Equals("Cancel"))
            {
                ClassInformationEditButtonLabel = "Edit";
                CanClickClassInformationSave = false;
                CanEditClassIfnormation = false;
            }
            ClassOpertionErrorMesage = "";
            NotifyPropertyChanged("");
        }
        #endregion

    }
}
