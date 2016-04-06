using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msdWPF.Model;
using msdWPF.View;

namespace msdWPF.ViewModel
{
    public class StudentViewModel : INotifyPropertyChanged
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

        private StudentModel _studentModel;

        #region StudentUC

        public List<String> FirstNameSearchList
        {
            get
            {
                if (null == _firstNameList && null != _studentModel)
                {
                    _firstNameList = _studentModel.FindAllStudentFirstName();
                }
                return _firstNameList;
            }
        }
        private List<String> _firstNameList;

        public String SearchFirstName { get; set; }

        public List<String> LastNameSearchList
        {
            get
            {
                if (null == _lastNameList && null != _studentModel)
                {
                    _lastNameList = _studentModel.FindAllStudentLastName();
                }
                return _lastNameList;
            }
        }
        private List<String> _lastNameList;

        public List<SchoolSemester> SemesterListForStudent
        {
            get
            {
                if (null == _semesterList && null != _studentModel)
                {
                    _semesterList = _studentModel.FindAllSemesterList();
                }
                return _semesterList;
            }
        }

        private List<SchoolSemester> _semesterList; 

        public String SearchLastName { get; set; }

        public string ShowStudentTabControl { get; set; }

        public String StudentSearchErrorMesage { get; set; }

        internal void SearchStudentByFirstLastName()
        {
            
            if (String.IsNullOrEmpty(SearchFirstName) || String.IsNullOrEmpty(SearchLastName))
            {
                StudentSearchErrorMesage = "Please Input First and Last Name.";
                NotifyPropertyChanged("");
                return;
            }
            MSDStudent student = _studentModel.FindStudentByFirstNameLastName(SearchFirstName.Trim(),
                SearchLastName.Trim());
            if (null == student)
            {
                StudentSearchErrorMesage = "Can't find student with first name : " + SearchFirstName.Trim() +
                                           " last name : " + SearchLastName.Trim();
                NotifyPropertyChanged("");
                return;
            }

            CurrentMSDStudent = student;

            ShowStudentTabControl = "Visible";
            StudentSearchErrorMesage = "";

            _msdStudentParents = _studentModel.FindStudentParentsByStudentId(CurrentMSDStudent.Id);

            StudentInformationEditButtonLabel = "Edit";
            CanEditStudentInformation = false;
            CanClickStudentInformationSave = false;

            _msdStudentMedical = _studentModel.FindStudentMedicalByStudentId(CurrentMSDStudent.Id);

            StudentMedicalEditButtonLabel = "Edit";
            CanEditStudentMedical = false;
            CanClickStudentMedicalSave = false;

            NotifyPropertyChanged("");
        }

        internal void SetStudentModel(StudentModel studentModel)
        {
            _studentModel = studentModel;
            ShowStudentTabControl = "Hidden";
            _lastNameList = _studentModel.FindAllStudentLastName();
            _firstNameList = _studentModel.FindAllStudentFirstName();
            _semesterList = _studentModel.FindAllSemesterList();
            NotifyPropertyChanged("");
        }

        internal void CancelSearchStudent()
        {
            SearchFirstName = "";
            SearchLastName = "";
            ShowStudentTabControl = "Hidden";
            StudentSearchErrorMesage = "";
            NotifyPropertyChanged("");
        }
        #endregion

        #region StudentInformationUC
        private MSDStudent _curMsdStudent;
        private List<MSDStudentParent> _msdStudentParents;
        private MSDStudentMedical _msdStudentMedical;

        public MSDStudent CurrentMSDStudent
        {
            get { return _curMsdStudent; }
            set
            {
                _curMsdStudent = value;

                if (null == _curMsdStudent) return;

                if (!(String.IsNullOrEmpty(_curMsdStudent.Gender)))
                {
                    if (_curMsdStudent.Gender.Equals("M"))
                    {
                        StudentIsFemale = false;
                        StudentIsMale = true;
                    }
                    else if (_curMsdStudent.Gender.Equals("F"))
                    {
                        StudentIsFemale = true;
                        StudentIsMale = false;
                    }
                    else
                    {
                        StudentIsFemale = false;
                        StudentIsMale = false;
                    }
                }
                else
                {
                    StudentIsFemale = false;
                    StudentIsMale = false;
                }
            }
        }

        public MSDStudentParent ParentOne
        {
            get
            {
                if (null != _msdStudentParents && _msdStudentParents.Count > 0)
                {
                    return _msdStudentParents[0];
                }
                return null;
            }
        }

        public MSDStudentParent ParentTwo
        {
            get
            {
                if (null != _msdStudentParents && _msdStudentParents.Count > 1)
                {
                    return _msdStudentParents[1];
                }
                return null;
            }
        }

        public MSDStudentMedical CurrentStudentMedical { get { return _msdStudentMedical; } }

        public bool CanEditStudentInformation { get; set; }
        public String StudentInformationEditButtonLabel { get; set; }
        public bool CanClickStudentInformationSave { get; set; }
        public bool CanEditStudentMedical { get; set; }
        public String StudentMedicalEditButtonLabel { get; set; }
        public bool CanClickStudentMedicalSave { get; set; }

        public bool StudentFirstNameChanged { get; set; }
        public bool StudentLastNameChanged { get; set; }

        public bool StudentIsMale { get; set; }

        public bool StudentIsFemale { get; set; }

        internal void EditStudentInformation()
        {
            if (StudentInformationEditButtonLabel.Equals("Edit"))
            {
                StudentInformationEditButtonLabel = "Cancel";
                CanEditStudentInformation = true;
                CanClickStudentInformationSave = true;
            }
            else if (StudentInformationEditButtonLabel.Equals("Cancel"))
            {
                MSDStudent student = _studentModel.FindStudentById(CurrentMSDStudent.Id);

                CurrentMSDStudent = student;
                
                if (null != CurrentMSDStudent && 0 != CurrentMSDStudent.Id)
                    _msdStudentParents = _studentModel.FindStudentParentsByStudentId(CurrentMSDStudent.Id);

                StudentInformationEditButtonLabel = "Edit";
                CanEditStudentInformation = false;
                CanClickStudentInformationSave = false;
                
            }
            StudentFirstNameChanged = false;
            StudentLastNameChanged = false;
            NotifyPropertyChanged("");
        }

        internal void EditStudentMedical()
        {
            if (StudentMedicalEditButtonLabel.Equals("Edit"))
            {
                StudentMedicalEditButtonLabel = "Cancel";
                CanEditStudentMedical = true;
                CanClickStudentMedicalSave = true;
            }
            else if (StudentMedicalEditButtonLabel.Equals("Cancel"))
            {
                if (null != CurrentMSDStudent && 0 != CurrentMSDStudent.Id)
                    _msdStudentMedical = _studentModel.FindStudentMedicalByStudentId(CurrentMSDStudent.Id);

                StudentMedicalEditButtonLabel = "Edit";
                CanEditStudentMedical = false;
                CanClickStudentMedicalSave = false;
            }
            StudentSearchErrorMesage = "";
            NotifyPropertyChanged("");
        }

        internal void SaveStudentInformation()
        {
            if (String.IsNullOrEmpty(CurrentMSDStudent.FirstName) ||
                String.IsNullOrEmpty(CurrentMSDStudent.LastName))
            {
                StudentSearchErrorMesage = "Please provide student First name and Last name.";
                NotifyPropertyChanged("");
                return;
            }

            if (StudentIsMale) CurrentMSDStudent.Gender = "M";
            if (StudentIsFemale) CurrentMSDStudent.Gender = "F";

            if (CurrentMSDStudent.Id == 0)
            {
                MSDStudent _newStudent = _studentModel.AddStudentInformation(CurrentMSDStudent);
                CurrentMSDStudent = _newStudent;
            }
            else
            {
                _studentModel.SaveStudentInformation(CurrentMSDStudent);
            }

            if (ParentOne.Id == 0)
                ParentOne.MSDStudentId = CurrentMSDStudent.Id;
            _studentModel.SaveStudentParent(ParentOne);

            if (ParentTwo.Id == 0)
                ParentTwo.MSDStudentId = CurrentMSDStudent.Id;
            _studentModel.SaveStudentParent(ParentTwo);

            if (StudentFirstNameChanged || StudentLastNameChanged)
            {
                _firstNameList = _studentModel.FindAllStudentFirstName();
                _lastNameList = _studentModel.FindAllStudentLastName();
                SearchFirstName = CurrentMSDStudent.FirstName;
                SearchLastName = CurrentMSDStudent.LastName;
            }

            StudentFirstNameChanged = false;
            StudentLastNameChanged = false;

            StudentInformationEditButtonLabel = "Edit";
            CanEditStudentInformation = false;
            CanClickStudentInformationSave = false;

            NotifyPropertyChanged("");
        }

        internal void SaveStudentMedical()
        {
            if (null == CurrentMSDStudent || 0 == CurrentMSDStudent.Id)
            {
                StudentSearchErrorMesage = "Please save student information first.";
                NotifyPropertyChanged("");
                return;
            }

            if (CurrentStudentMedical.Id != 0)
                CurrentStudentMedical.MSDStudentId = CurrentMSDStudent.Id;

            _studentModel.SaveStudentMedical(CurrentStudentMedical);

            StudentMedicalEditButtonLabel = "Edit";
            CanEditStudentMedical = false;
            CanClickStudentMedicalSave = false;
            NotifyPropertyChanged("");
        }

        internal void SetCurrentStudent(MSDStudent student)
        {
            CurrentMSDStudent = student;

            StudentMedicalEditButtonLabel = "Edit";
            CanEditStudentMedical = false;
            CanClickStudentMedicalSave = false;
            NotifyPropertyChanged("");
        }

        internal void FoundSearchStudent(MSDStudent student)
        {
            CurrentMSDStudent = student;

            StudentInformationEditButtonLabel = "Edit";
            CanEditStudentInformation = false;
            CanClickStudentInformationSave = false;

            StudentMedicalEditButtonLabel = "Edit";
            CanEditStudentMedical = false;
            CanClickStudentMedicalSave = false;

            NotifyPropertyChanged("");
        }

        internal void AddNewStudent()
        {
            CurrentMSDStudent = new MSDStudent();

            ShowStudentTabControl = "Visible";
            StudentSearchErrorMesage = "";
            SearchFirstName = "";
            SearchLastName = "";

            StudentInformationEditButtonLabel = "Cancel";
            CanEditStudentInformation = true;
            CanClickStudentInformationSave = true;

            StudentMedicalEditButtonLabel = "Cancel";
            CanEditStudentMedical = true;
            CanClickStudentMedicalSave = true;

            _msdStudentMedical = new MSDStudentMedical();
            _msdStudentParents = new List<MSDStudentParent>();
            _msdStudentParents.Add(new MSDStudentParent());
            _msdStudentParents.Add(new MSDStudentParent());
            NotifyPropertyChanged("");
        }

        #endregion

    }
}
