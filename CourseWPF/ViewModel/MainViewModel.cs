using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CourseWPF.Model;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CourseWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Organization selectedOrganization;
        private Member selectedMember;
        private Event selectedEvent;
        private Member selectedNewEventMember;
        private Member selectedNewEventMemberInsert;

        public ObservableCollection<Organization> Organizations { get; set; }
        public ObservableCollection<Member> Members { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        public Member SelectedNewEventMember
        {
            get { return selectedNewEventMember; }
            set
            {
                selectedNewEventMember = value;
                OnPropertyChanged("SelectedNewEventMember");
            }
        }

        public Member SelectedNewEventMemberInsert
        {
            get { return selectedNewEventMemberInsert; }
            set
            {
                SelectedNewEventMemberInsert = value;
                OnPropertyChanged("SelectedNewEventMemberInsert");
            }
        }

        public Organization SelectedOrganization
        {
            get { return selectedOrganization; }
            set
            {
                selectedOrganization = value;
                OnPropertyChanged("SelectedOrganization");
            }
        }

        public Member SelectedMember
        {
            get { return selectedMember; }
            set
            {
                selectedMember = value;
                OnPropertyChanged("SelectedMember");
                OnPropertyChanged("FilteredMembers");
            }
        }

        private string newMemberName;
        public string NewMemberName
        {
            get { return newMemberName; }
            set
            {
                newMemberName = value;
                OnPropertyChanged("NewMemberName");
                FilterMembersByOrganization();
            }
        }

        private string newOrganizationName;
        public string NewOrganizationName
        {
            get { return newOrganizationName; }
            set
            {
                newOrganizationName = value;
                OnPropertyChanged("NewOrganizationName");
            }
        }

        private Organization newMemberOrganization;
        public Organization NewMemberOrganization
        {
            get { return newMemberOrganization; }
            set
            {
                newMemberOrganization = value;
                FilterMembersByOrganization();
                OnPropertyChanged("NewMemberOrganization");
            }
        }

        private Organization newEventOrganization;
        public Organization NewEventOrganization
        {
            get { return newEventOrganization; }
            set
            {
                newEventOrganization = value;
                FilterEventsByOrganization();
     
                OnPropertyChanged("NewEventOrganization");
                FilteredEventOrganizations();
            }
        }


        private DateTime? newEventDate;
        public DateTime? NewEventDate
        {
            get { return newEventDate; }
            set
            {
                newEventDate = value;
                OnPropertyChanged("NewEventDate");

            }
        }

        private ObservableCollection<Member> newEventMembers;
        public ObservableCollection<Member> NewEventMembers
        {
            get { return newEventMembers; }
            set
            {
                newEventMembers = value;
                OnPropertyChanged("NewEventMembers");
            }
        }

        private void FilteredEventOrganizations()
        {
            if (NewEventOrganization != null)
            {
                NewEventMembers = new ObservableCollection<Member>(
                Members.Where(member =>
                    member.MemberOrganization == NewEventOrganization.OrganizationName)
                );
            }
            else
            {
                NewEventMembers = Members;
            }
        }


        private ObservableCollection<Member> filteredMembers;
        public ObservableCollection<Member> FilteredMembers
        {
            get { return filteredMembers; }
            set
            {
                filteredMembers = value;
                OnPropertyChanged("FilteredMembers");
            }
        }

        private void FilterMembersByOrganization()
        {
            if (NewMemberOrganization != null)
            {
                FilteredMembers = new ObservableCollection<Member>(
                Members.Where(member =>
                    member.MemberOrganization == NewMemberOrganization.OrganizationName)
                );
            }
            else
            {
                FilteredMembers = Members;
            }
        }

        private ObservableCollection<Event> filteredevents;
        public ObservableCollection<Event> FilteredEvents
        {
            get { return filteredevents; }
            set
            {
                filteredevents = value;
                OnPropertyChanged("FilteredEvents");
            }
        }


        private void FilterEventsByOrganization()
        {
            if (NewEventOrganization != null)
            {
                FilteredEvents = new ObservableCollection<Event>(
                Events.Where(ev =>
                    ev.EventOrganization == NewEventOrganization.OrganizationName)
                );
            }
            else
            {
                FilteredEvents = Events;
            }
        }


        private string newEventName;
        public string NewEventName
        {
            get { return newEventName; }
            set
            {
                newEventName = value;
                OnPropertyChanged("NewEventName");
            }
        }


        public ICommand AddCommandO { get; set; }
        public ICommand DelCommandO { get; set; }
        public ICommand AddCommandM { get; set; }
        public ICommand DelCommandM { get; set; }
        public ICommand AddCommandE { get; set; }
        public ICommand DelCommandE { get; set; }

        public MainViewModel()
        {

            Organizations = new ObservableCollection<Organization>
            {
                new Organization {OrganizationId = 1, OrganizationName="Apple"},
                new Organization {OrganizationId = 2, OrganizationName="Samsung"},
            };
            Members = new ObservableCollection<Member>
            {
                new Member {MemberId = 1, MemberName="Baker A. P.", MemberOrganization= "Apple"},
                new Member {MemberId = 2, MemberName="Tkyah D. H", MemberOrganization="Samsung"},
            };
            Events = new ObservableCollection<Event> 
            {
                new Event { EventId = 1, EventName = "Event 1", EventOrganization = "Apple", EventMember = "Baker A. P.", EventDate = new DateTime(2023, 11, 30, 9, 0, 0) },
                new Event { EventId = 2, EventName = "Event 2", EventOrganization = "Samsung", EventMember = "Tkyah D. H.", EventDate = new DateTime(2023, 12, 5, 13, 30, 0) }
            };

            AddCommandO = new RelayCommand(AddOrganization);
            AddCommandM = new RelayCommand(AddMember);
            DelCommandO = new RelayCommand(DelOrganization);
            DelCommandM = new RelayCommand(DelMember);
            AddCommandE = new RelayCommand(AddEvent);
            DelCommandE = new RelayCommand(DelEvent);
            FilterMembersByOrganization();
            FilterEventsByOrganization();
            FilteredEventOrganizations();
        }

        private void DelEvent(object parameter)
        {
            if (SelectedEvent != null)
            {
                Events.Remove(SelectedEvent);
                SelectedEvent = null;
                FilterEventsByOrganization();
            }
            else
            {
                SelectedNewEventMember = null;
                SystemSounds.Beep.Play();
                MessageBox.Show("Выберите участника для удаления", "Ошибка");
            }
        }

        private void AddEvent(object parameter)
        {
            if (NewEventName != null && NewEventDate != null && NewEventOrganization != null && SelectedNewEventMember != null)
            {
                int newId;
                if (Events.Count == 0)
                {
                    newId = 1;
                }
                else
                {
                    newId = Events.Max(Event => Event.EventId) + 1;
                }

                List<string> SelectedNewEventMemberInsert = NewEventMembers.Select(member => member.MemberName).ToList();
               
                Event newEvent = new Event()
                {
                    EventId = newId,
                    EventName = NewEventName,
                    EventMember = string.Join(", \n", SelectedNewEventMemberInsert),
                    EventOrganization = NewEventOrganization.OrganizationName, 
                    EventDate = NewEventDate.Value

                };


                SelectedNewEventMember = null;
                NewEventOrganization = null;
                SelectedEvent = newEvent;
                Events.Add(newEvent);
                NewEventName = null; 
                NewEventDate = null;
                FilterEventsByOrganization();
            }
            else
            {
                NewEventOrganization = null;
                SelectedEvent = null;
                SystemSounds.Beep.Play();
                MessageBox.Show("Введите имя события, выберите дату, организацию и участников", "Ошибка");
            }
        }

        private void DelMember(object parameter)
        {
            if (SelectedMember != null)
            {
                Members.Remove(SelectedMember);
                SelectedMember = null;
                FilterMembersByOrganization();

            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Выберите участника для удаления", "Ошибка");
            }
        }

        private void DelOrganization(object parameter)
        {
            if (SelectedOrganization != null)
            {
                bool membersExist = Members.Any(member => member.MemberOrganization == SelectedOrganization.OrganizationName);

                if (!membersExist)
                {
                    Organizations.Remove(SelectedOrganization);
                    SelectedOrganization = null;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("В вашей организации есть люди", "Ошибка");
                }
            }
            else
            {
                NewEventOrganization = null;
                SystemSounds.Beep.Play();
                MessageBox.Show("Выберите организацию для удаления", "Ошибка");
            }
        }

        private void AddOrganization(object parameter)
        {
            if (NewOrganizationName != null)
            {
                int newId;
                if (Organizations.Count == 0)
                {
                    newId = 1;
                }
                else
                {
                    newId = Organizations.Max(Organization => Organization.OrganizationId) + 1;
                }
                Organization newOrganization = new Organization() { OrganizationId = newId, OrganizationName = NewOrganizationName };
                Organizations.Add(newOrganization);
                NewOrganizationName = null;
            }
            else
            {
                SelectedOrganization = null;
                SystemSounds.Beep.Play();
                MessageBox.Show("Введите название организации", "Ошибка");
            }
        }

        private void AddMember(object parameter)
        {
            if (NewMemberName != null && NewMemberOrganization != null)
            {
                int newId;
                if (Members.Count == 0)
                {
                    newId = 1;
                }
                else
                {
                    newId = Members.Max(Member => Member.MemberId) + 1;
                }
                Member newMember = new Member() { MemberId = newId, MemberName = NewMemberName, MemberOrganization = NewMemberOrganization.OrganizationName };
                Members.Add(newMember);
                NewMemberName = null;
                SelectedMember = newMember;
                FilterMembersByOrganization();
            }
            else
            {
                NewMemberOrganization = null;
                SelectedMember = null;
                SystemSounds.Beep.Play();
                MessageBox.Show("Введите имя и укажите организацию", "Ошибка");
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

