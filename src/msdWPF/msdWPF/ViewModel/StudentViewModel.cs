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

        private StudentModel _studentModel;

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
                
                _msdStudentParents = _studentModel.FindStudentParentsByStudentId(CurrentMSDStudent.Id);

                StudentInformationEditButtonLabel = "Edit";
                CanEditStudentInformation = false;
                CanClickStudentInformationSave = false;
                
            }
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
                _msdStudentMedical = _studentModel.FindStudentMedicalByStudentId(CurrentMSDStudent.Id);

                StudentMedicalEditButtonLabel = "Edit";
                CanEditStudentMedical = false;
                CanClickStudentMedicalSave = false;
            }
            NotifyPropertyChanged("");
        }

        internal void SaveStudentInformation()
        {

            if (StudentIsMale) CurrentMSDStudent.Gender = "M";
            if (StudentIsFemale) CurrentMSDStudent.Gender = "F";

            _studentModel.SaveStudentInformation(CurrentMSDStudent);
            _studentModel.SaveStudentParent(ParentOne);
            _studentModel.SaveStudentParent(ParentTwo);

            StudentInformationEditButtonLabel = "Edit";
            CanEditStudentInformation = false;
            CanClickStudentInformationSave = false;

            NotifyPropertyChanged("");
        }

        internal void SaveStudentMedical()
        {
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

            NotifyPropertyChanged("StudentInformationEditButtonLabel");
        }

        #endregion


    }
}
