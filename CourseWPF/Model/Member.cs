using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseWPF.Model
{
    public class Member : INotifyPropertyChanged
    {
        private int memberId;
        private string memberName;
        private string memberOrganization;

        public int MemberId
        {
            get { return memberId; }
            set
            {
                memberId = value;
                OnPropertyChanged("OrganizationId");
            }
        }

        public string MemberName
        {
            get { return memberName; }
            set
            {
                memberName = value;
                OnPropertyChanged("OrganizationName");
            }
        }

        public string MemberOrganization
        {
            get { return memberOrganization; }
            set
            {
                memberOrganization = value;
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
