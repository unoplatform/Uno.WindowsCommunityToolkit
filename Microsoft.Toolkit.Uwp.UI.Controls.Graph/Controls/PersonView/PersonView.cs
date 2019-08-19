using Microsoft.Graph;
using Microsoft.Toolkit.Uwp.UI.Controls.Graph.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Microsoft.Toolkit.Uwp.UI.Controls.Graph
{
    public partial class PersonView : Control
    {
        public const string PersonQueryMe = "me";

        /// <summary>
        /// Stores details about this person retrieved from the graph or provided by the developer.
        /// </summary>
        public Person PersonDetails
        {
            get { return (Person)GetValue(PersonDetailsProperty); }
            set { SetValue(PersonDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Person.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonDetailsProperty =
            DependencyProperty.Register(nameof(PersonDetails), typeof(Person), typeof(PersonView), new PropertyMetadata(null, PersonDetailsPropertyChanged));

        /// <summary>
        /// Set to automatically retrieve data on the specified query from the graph.  Use <see cref="PersonQueryMe"/> to retrieve info about the current user.  Otherwise, it's best to use an e-mail address as a query.
        /// </summary>
        public string PersonQuery
        {
            get { return (string)GetValue(PersonQueryProperty); }
            set { SetValue(PersonQueryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonQuery.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonQueryProperty =
            DependencyProperty.Register(nameof(PersonQuery), typeof(string), typeof(PersonView), new PropertyMetadata(null));

        /// <summary>
        /// Use to look up the specific user with the provided Id.
        /// </summary>
        public string UserId
        {
            get { return (string)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserIdProperty =
            DependencyProperty.Register(nameof(UserId), typeof(string), typeof(PersonView), new PropertyMetadata(null));

        public bool ShowName
        {
            get { return (bool)GetValue(ShowNameProperty); }
            set { SetValue(ShowNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowNameProperty =
            DependencyProperty.Register(nameof(ShowName), typeof(bool), typeof(PersonView), new PropertyMetadata(false, ShowDisplayPropertiesChanged));

        public bool ShowEmail
        {
            get { return (bool)GetValue(ShowEmailProperty); }
            set { SetValue(ShowEmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowEmail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowEmailProperty =
            DependencyProperty.Register(nameof(ShowEmail), typeof(bool), typeof(PersonView), new PropertyMetadata(false, ShowDisplayPropertiesChanged));

        public BitmapImage UserPhoto
        {
            get { return (BitmapImage)GetValue(UserPhotoProperty); }
            set { SetValue(UserPhotoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserPhoto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserPhotoProperty =
            DependencyProperty.Register(nameof(UserPhoto), typeof(BitmapImage), typeof(PersonView), new PropertyMetadata(null));



        public string Initials
        {
            get { return (string)GetValue(InitialsProperty); }
            internal set { SetValue(InitialsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Initials.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialsProperty =
            DependencyProperty.Register(nameof(Initials), typeof(string), typeof(PersonView), new PropertyMetadata(""));



        public bool IsLargeImage
        {
            get { return (bool)GetValue(IsLargeImageProperty); }
            internal set { SetValue(IsLargeImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLargeImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLargeImageProperty =
            DependencyProperty.Register(nameof(IsLargeImage), typeof(bool), typeof(PersonView), new PropertyMetadata(false));

        private string _photoId = null;

        private static void PersonDetailsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PersonView pv)
            {
                if (pv.PersonDetails != null)
                {
                    if (pv.PersonDetails.GivenName?.Length > 0 && pv.PersonDetails.Surname?.Length > 0)
                    {
                        pv.Initials = string.Empty + pv.PersonDetails.GivenName[0] + pv.PersonDetails.Surname[0];
                    }
                    else if (pv.PersonDetails.DisplayName?.Length > 0)
                    {
                        // Grab first two initials in name
                        var initials = pv.PersonDetails.DisplayName.ToUpper().Split(' ').Select(i => i.First());
                        pv.Initials = string.Join(string.Empty, initials.Where(i => char.IsLetter(i)).Take(2));
                    }

                    if (pv.UserPhoto == null || pv.PersonDetails.Id != pv._photoId)
                    {
                        // Reload Image
                        pv.UserPhoto = null;
                        pv.LoadImageAsync(pv.PersonDetails);
                    }
                    else if (pv.PersonDetails.Id != pv._photoId)
                    {
                        pv.UserPhoto = null;
                        pv._photoId = null;
                    }
                }
            }
        }

        private static void ShowDisplayPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PersonView pv)
            {
                pv.IsLargeImage = pv.ShowName && pv.ShowEmail;
            }
        }

        public PersonView()
        {
            this.DefaultStyleKey = typeof(PersonView);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            LoadData();
        }

        private async void LoadData()
        {
            var graph = GlobalProvider.Instance.Graph;
            if (graph != null)
            {
                if (PersonDetails != null && UserPhoto == null)
                {
                    LoadImageAsync(PersonDetails);
                }
                else if (!string.IsNullOrWhiteSpace(UserId) || PersonQuery?.ToLowerInvariant() == PersonQueryMe)
                {
                    User user = null;
                    if (!string.IsNullOrWhiteSpace(UserId))
                    {
                        try
                        {
                            user = await graph.Users[UserId].Request().GetAsync();
                        }
                        catch { }
                        try
                        {
                            // TODO: Move to LoadImage based on previous call?
                            DecodeStreamAsync(await graph.Users[UserId].Photo.Content.Request().GetAsync());
                            _photoId = UserId;
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            user = await graph.Me.Request().GetAsync();
                        }
                        catch { }
                        try
                        {
                            DecodeStreamAsync(await graph.Me.Photo.Content.Request().GetAsync());
                            _photoId = user.Id;
                        }
                        catch { }
                    }

                    if (user != null)
                    {
                        PersonDetails = new Person()
                        {
                            DisplayName = user.DisplayName,
                            ScoredEmailAddresses = new ScoredEmailAddress[] { new ScoredEmailAddress()
                        {
                            Address = user.Mail ?? user.UserPrincipalName
                        }},
                            GivenName = user.GivenName,
                            Surname = user.Surname
                        };
                    }
                }
                else if (PersonDetails == null && !string.IsNullOrWhiteSpace(PersonQuery))
                {
                    // TODO: https://github.com/microsoftgraph/microsoft-graph-toolkit/blob/master/src/components/mgt-person/mgt-person.ts#L101
                }
            }
        }

        private async void LoadImageAsync(Person person)
        {
            try
            {
                var graph = GlobalProvider.Instance.Graph;

                if (!string.IsNullOrWhiteSpace(person.UserPrincipalName))
                {
                    DecodeStreamAsync(await graph.GetUserPhoto(person.UserPrincipalName));
                    _photoId = person.Id; // TODO: Only set on success for photo?
                }
                else if (!string.IsNullOrWhiteSpace(person.ScoredEmailAddresses.First().Address))
                {
                    // TODO https://github.com/microsoftgraph/microsoft-graph-toolkit/blob/master/src/components/mgt-person/mgt-person.ts#L126
                }
            }
            catch
            {
                // If we can't load a photo, that's ok.
            }
        }

        private async void DecodeStreamAsync(Stream photoStream)
        {
            if (photoStream != null)
            {
                using (var ras = photoStream.AsRandomAccessStream())
                {
                    var bitmap = new BitmapImage();
                    await bitmap.SetSourceAsync(ras);
                    UserPhoto = bitmap;
                }
            }
        }
    }
}
