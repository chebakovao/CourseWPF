using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseWPF.Model
{
    public class Event : INotifyPropertyChanged
    {
        private int eventId;
        private string eventName;
        private string eventOrganization;
        private string eventMember;
        private DateTime? eventDate;

        public int EventId
        {
            get { return eventId; }
            set
            {
                eventId = value;
                OnPropertyChanged("EventId");
            }
        }

        public string EventName
        {
            get { return eventName; }
            set
            {
                eventName = value;
                OnPropertyChanged("EventName");
            }
        }

        public string EventOrganization
        {
            get { return eventOrganization; }
            set
            {
                eventOrganization = value;
                OnPropertyChanged("EventOrganization");
            }
        }
        public string EventMember
        {
            get { return eventMember; }
            set
            {
                eventMember = value;
                OnPropertyChanged("EventMember");
            }
        }

        public DateTime? EventDate
        {
            get { return eventDate; }
            set
            {
                eventDate = value;
                OnPropertyChanged("EventDate");
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
