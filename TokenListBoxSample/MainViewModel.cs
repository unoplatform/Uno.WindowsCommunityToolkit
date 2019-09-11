using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TokenListBoxSample.Common;
using TokenListBoxSample.Models;

namespace TokenListBoxSample
{
    public class MainViewModel : PropertyChangedNotifier
    {
        public DelegateCommand<string> QuerySubmittedCommand { get; }
        public DelegateCommand<ContactModel> SuggestionChosenCommand { get; }
        public DelegateCommand<string> TextChangedCommand { get; }

        private string _previousQuery;
        private readonly List<ContactModel> _allContacts;
        private List<ContactModel> _chosenContacts;

        private string _queryText;
        public string QueryText
        {
            get => _queryText;
            set => Set(ref _queryText, value);
        }

        private ObservableCollection<ContactModel> _suggestedContacts;
        public ObservableCollection<ContactModel> SuggestedContacts
        {
            get => _suggestedContacts;
            set => Set(ref _suggestedContacts, value);
        }


        public MainViewModel()
        {
            QuerySubmittedCommand = new DelegateCommand<string>(OnQuerySubmitted);
            SuggestionChosenCommand = new DelegateCommand<ContactModel>(OnSuggestionChosen);
            TextChangedCommand = new DelegateCommand<string>(OnTextChanged);

            _suggestedContacts = new ObservableCollection<ContactModel>();
            _chosenContacts = new List<ContactModel>();
            _allContacts = new List<ContactModel>() {
                new ContactModel("Assets/Contacts/Contact1.jpg", "John", "Smith"),
                new ContactModel("Assets/Contacts/Contact2.jpg", "Autumn", "Ferris"),
                new ContactModel("Assets/Contacts/Contact3.jpg", "Todd", "Mathison"),
            };
        }

        private void OnQuerySubmitted(string query)
        {
            if (_suggestedContacts.Count > 0)
            {
                OnSuggestionChosen(_suggestedContacts[0]);
            }
        }

        private void OnSuggestionChosen(ContactModel contact)
        {
            _chosenContacts.Add(contact);
            SuggestedContacts.Clear();
        }

        /// <summary>
        /// Filter contacts when the query text changes
        /// </summary>
        /// <param name="text"></param>
        private void OnTextChanged(string text)
        {
            // If empty, clear out
            if (string.IsNullOrWhiteSpace(text))
            {
                SuggestedContacts.Clear();
            }
            // If we are typing, continue to reduce the set
            else if (!string.IsNullOrWhiteSpace(_previousQuery) && text.StartsWith(_previousQuery))
            {
                int count = _suggestedContacts.Count;
                for(var i = count - 1; i >= 0; i--)
                {
                    var contact = _suggestedContacts[i];
                    if (!contact.DisplayName.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        SuggestedContacts.Remove(contact);
                    }
                }
            }
            // Else, filter all contacts
            else
            {
                SuggestedContacts.Clear();

                foreach (var contact in _allContacts)
                {
                    if (contact.DisplayName.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        SuggestedContacts.Add(contact);
                    }
                }
            }

            _previousQuery = text;
        }
    }
}
