using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseWPF.Model
{
    public class Organization : INotifyPropertyChanged
    {
        private int organizationId;
        private string organizationName;

        public int OrganizationId
        {
            get { return organizationId; }
            set
            {
                organizationId = value;
                OnPropertyChanged("OrganizationId");
            }
        }

        public string OrganizationName
        {
            get { return organizationName; }
            set
            {
                organizationName = value;
                OnPropertyChanged("OrganizationName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
