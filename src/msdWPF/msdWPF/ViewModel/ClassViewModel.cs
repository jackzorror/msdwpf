using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Controls;
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

            WeekDayList = new Dictionary<string, object>();
            WeekDayList.Add("Sunday", "SUN");
            WeekDayList.Add("Monday", "MON");
            WeekDayList.Add("Tuesday", "TUE");
            WeekDayList.Add("Wednseday", "WED");
            WeekDayList.Add("Thursday", "THU");
            WeekDayList.Add("Friday", "FRI");
            WeekDayList.Add("Saturday", "SAT");

            HourList = new List<string>();
            HourList.Add("8:00");
            HourList.Add("9:00");
            HourList.Add("10:00");
            HourList.Add("11:00");
            HourList.Add("12:00");
            HourList.Add("13:00");
            HourList.Add("14:00");
            HourList.Add("15:00");
            HourList.Add("16:00");
            HourList.Add("17:00");
            HourList.Add("18:00");
            HourList.Add("19:00");
            HourList.Add("20:00");
            HourList.Add("21:00");

            MinuteList = new List<string>();
            MinuteList.Add("00");
            MinuteList.Add("05");
            MinuteList.Add("10");
            MinuteList.Add("15");
            MinuteList.Add("20");
            MinuteList.Add("25");
            MinuteList.Add("30");
            MinuteList.Add("35");
            MinuteList.Add("40");
            MinuteList.Add("45");
            MinuteList.Add("50");
            MinuteList.Add("55");
            NotifyPropertyChanged("");
        }

        public String ClassOpertionErrorMesage { get; set; }

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

        public SchoolSemester SelectedSchoolSemester { get; set; }

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

        public ApplicationType SelectedClassType { get; set; }

        public bool CanSelecteCurrentClass { get { return (null != SelectedClassType && null != SelectedSchoolSemester); } }
        public bool CanAddNewClass { get { return (null != SelectedClassType && null != SelectedSchoolSemester); } }
        public bool CanSearchClass {get {return (null != SelectedClassType && null != SelectedSchoolSemester && null != SelectedSchoolClassSummary); } }

        public List<SchoolClassSummary> SchoolClassSummaryList
        {
            get
            {
                if (null == _schoolClassSummaryList && null != _classModel)
                {
                    _schoolClassSummaryList = _classModel.FindAllSchoolClassSummary();
                }

                List<SchoolClassSummary> classes = null;
                if (null != SelectedSchoolSemester && null != SelectedClassType)
                {
                    classes = _schoolClassSummaryList.FindAll(x => x.SemesterId == SelectedSchoolSemester.Id).FindAll(x => x.ClassTypeId == SelectedClassType.Id);
                } 
                else if (null != SelectedSchoolSemester)
                {
                    classes = _schoolClassSummaryList.FindAll(x => x.SemesterId == SelectedSchoolSemester.Id);
                }
                else if (null != SelectedClassType)
                {
                    classes = _schoolClassSummaryList.FindAll(x => x.ClassTypeId == SelectedClassType.Id);
                }
                else
                {
                    classes = _schoolClassSummaryList;
                }

                return classes;
            }
        }

        private List<SchoolClassSummary> _schoolClassSummaryList;

        public SchoolClassSummary SelectedSchoolClassSummary
        {
            get { return _selectedSchoolClassSummary; }
            set
            {
                _selectedSchoolClassSummary = value;

                if (null != _selectedSchoolClassSummary)
                {
                    if (_currentSelectedClass == null || _selectedSchoolClassSummary.Id != _currentSelectedClass.Id)
                    {
                        _currentSelectedClass = _classModel.FindSchoolClassById(_selectedSchoolClassSummary.Id);
                    }
                }
                else
                {
                    _currentSelectedClass = null;
                }

                if (null != _currentSelectedClass)
                {
                    NonClassDateList = _classModel.FindNonClassDateListByClassId(_currentSelectedClass.Id);
                    NonClassDateListString = CreateNonClassDateListString(NonClassDateList);

                    ClassOpertionErrorMesage = "";
                    ShowClassInformationPanel = "Visible";
                    ClassInformationEditButtonLabel = "Edit";
                    CanEditClassIfnormation = false;
                    CanClickClassInformationSave = false;
                }
                else
                {
                    ClassOpertionErrorMesage = "";
                    ShowClassInformationPanel = "Hidden";
                }
                NotifyPropertyChanged("");
            }
        }

        private SchoolClassSummary _selectedSchoolClassSummary;

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
            _currentSelectedClass = new SchoolClass();
            _currentSelectedClass.Name = "New School Class";
            _currentSelectedClass.ClassTypeId = SelectedClassType.Id;
            _currentSelectedClass.ClassTypeName = SelectedClassType.Name;
            _currentSelectedClass.SemesterId = SelectedSchoolSemester.Id;
            _currentSelectedClass.SemesterName = SelectedSchoolSemester.Name;
            SchoolClassSummary newClassSummary = new SchoolClassSummary();
            newClassSummary.Id = _currentSelectedClass.Id;
            newClassSummary.Name = _currentSelectedClass.Name;
            newClassSummary.SemesterId = _currentSelectedClass.SemesterId;
            newClassSummary.ClassTypeId = _currentSelectedClass.ClassTypeId;

            SelectedSchoolClassSummary = newClassSummary;
            _schoolClassSummaryList.Add(newClassSummary);

            ClassOpertionErrorMesage = "";
            ShowClassInformationPanel = "Visible";
            ClassInformationEditButtonLabel = "Cancel";
            CanEditClassIfnormation = true;
            CanClickClassInformationSave = true;
            NonClassDateListString = null;
            NonClassDateList = null;
            NotifyPropertyChanged("");
        }


        internal void ClearSelectSchoolClass()
        {
            SelectedClassType = null;
            SelectedSchoolSemester = null;
            _currentSelectedClass = null;
            _schoolClassSummaryList = null;

            ClassOpertionErrorMesage = "";
            ShowClassInformationPanel = "Hidden";
            ClassInformationEditButtonLabel = "Edit";
            CanEditClassIfnormation = false;
            CanClickClassInformationSave = false;
            NonClassDateListString = null;
            NonClassDateList = null;

            NotifyPropertyChanged("");
        }

        internal void SearchSchoolClass()
        {
            _currentSelectedClass = _classModel.FindSchoolClassById(_selectedSchoolClassSummary.Id);

            ClassOpertionErrorMesage = "";
            ShowClassInformationPanel = "Visible";
            ClassInformationEditButtonLabel = "Edit";
            CanEditClassIfnormation = false;
            CanClickClassInformationSave = false;

            NotifyPropertyChanged("");
        }

        #endregion

        #region Class Information

        private List<NonClassDate> _addNonClassDateList = new List<NonClassDate>();
        private List<NonClassDate> _deleteNonClassDateList = new List<NonClassDate>();

        public String ClassInformationEditButtonLabel { get; set; }
        public bool CanClickClassInformationSave { get; set; }
        public String ShowClassInformationPanel { get; set; }
        public bool CanEditClassIfnormation { get; set; }
        public String NonClassDateListString { get; set; }
        public List<NonClassDate> NonClassDateList { get; set; }

        public bool CanAddNonClassDate
        {
            get { return SelectedNonClassDateForAdd != null; } }

        public DateTime? SelectedNonClassDateForAdd
        {
            get { return _selectedNonClassDateForAdd; }
            set { _selectedNonClassDateForAdd = value; NotifyPropertyChanged("CanAddNonClassDate"); }
        }
        private DateTime? _selectedNonClassDateForAdd;

        public bool CanDeleteNonClassDate
        {
            get { return SelectedNonClassDateForDelete != null; } 
        }
        public NonClassDate SelectedNonClassDateForDelete
        {
            get { return _selectedNonClassDateForDelete; }
            set { _selectedNonClassDateForDelete = value; NotifyPropertyChanged(""); }
        }

        private NonClassDate _selectedNonClassDateForDelete = null;

        public SchoolClass CurrentSelectedClass { get { return _currentSelectedClass; } }

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
                if (null == CurrentSelectedClass.Id || 0 == CurrentSelectedClass.Id)
                {
                    ClearSelectSchoolClass();
                }
            }
            ClassOpertionErrorMesage = "";
            NotifyPropertyChanged("");
        }

        internal void SaveCurrentSchoolClass()
        {
            if(null == CurrentSelectedClass.Id || 0 == CurrentSelectedClass.Id)
                _classModel.AddSchoolClass(CurrentSelectedClass);
            else
                _classModel.SaveSchoolClass(CurrentSelectedClass);

            foreach (SchoolClassSummary classSummary in SchoolClassSummaryList)
            {
                if (classSummary.Id == CurrentSelectedClass.Id)
                {
                    classSummary.Name = CurrentSelectedClass.Name;
                }
            }

            ClassInformationEditButtonLabel = "Edit";
            CanClickClassInformationSave = false;
            CanEditClassIfnormation = false;

            NotifyPropertyChanged("");
        }

        internal void EditNonClassDate()
        {
            _addNonClassDateList.Clear();
            _deleteNonClassDateList.Clear();
            SelectedNonClassDateForAdd = null;
            NotifyPropertyChanged("");
        }

        internal void AddNonClassDate()
        {
            if (null == NonClassDateListString) return;

            if (!NonClassDateList.Any(item => item.NonClassDateTime.Equals(SelectedNonClassDateForAdd)))
            {
                NonClassDate newDate = new NonClassDate();
                newDate.ClassId = CurrentSelectedClass.Id;
                newDate.NonClassDateTime = SelectedNonClassDateForAdd;
                _addNonClassDateList.Add(newDate);
                NonClassDateList.Add(newDate);
                NotifyPropertyChanged("NonClassDateList");
            }
        }
        #endregion

        #region Edit Class Scheduler

        public Dictionary<String, Object> WeekDayList { get; set; }
        public List<String> HourList { get; set; }
        public List<String> MinuteList { get; set; }

        #endregion

        internal void CancelEditNonClassDate()
        {
            _addNonClassDateList.Clear();
            _deleteNonClassDateList.Clear();
            NonClassDateList = _classModel.FindNonClassDateListByClassId(_currentSelectedClass.Id);
            NonClassDateListString = CreateNonClassDateListString(NonClassDateList);
            NotifyPropertyChanged("");
        }

        internal void SaveEditNonClassDate()
        {
            foreach (var nonClassDate in _addNonClassDateList)
            {
                _classModel.AddNonClassDate(nonClassDate);
            }
            foreach (var nonClassDate in _deleteNonClassDateList)
            {
                _classModel.DeleteNonClassDate(nonClassDate);
            }
            NonClassDateList = _classModel.FindNonClassDateListByClassId(_currentSelectedClass.Id);
            NonClassDateListString = CreateNonClassDateListString(NonClassDateList);
            NotifyPropertyChanged("");
        }

        private string CreateNonClassDateListString(List<NonClassDate> dateList)
        {
            String str = "";
            foreach (var date in dateList)
            {
                str += ((DateTime)date.NonClassDateTime).ToString("MM/dd/yyyy") + ";";
            }
            return str;
        }

        internal void DeletSelectedNonClassDate()
        {
            if (null == SelectedNonClassDateForDelete) return;

            NonClassDateList = (from item in NonClassDateList
                where item.Id != SelectedNonClassDateForDelete.Id ||
                      item.ClassId != SelectedNonClassDateForDelete.ClassId ||
                      item.NonClassDateTime != SelectedNonClassDateForDelete.NonClassDateTime
                select item).ToList();

            if (!_addNonClassDateList.Any(item => item.NonClassDateTime.Equals(SelectedNonClassDateForDelete.NonClassDateTime)))
            {
                _deleteNonClassDateList.Add(SelectedNonClassDateForDelete);
            }
            else
            {
                _addNonClassDateList.Remove(
                    _addNonClassDateList.Find(item => item.NonClassDateTime.Equals(SelectedNonClassDateForDelete.NonClassDateTime)));
            }

            SelectedNonClassDateForDelete = null;
            NotifyPropertyChanged("");
        }

        internal void EditClassScheduler()
        {
        }
    }
}
