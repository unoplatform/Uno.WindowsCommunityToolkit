using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace TokenListBoxSample.Controls
{
    public partial class TokenListBox : Control
    {
        public event TypedEventHandler<AutoSuggestBox, AutoSuggestBoxTextChangedEventArgs> QueryTextChanged;

        public event TypedEventHandler<AutoSuggestBox, AutoSuggestBoxSuggestionChosenEventArgs> SuggestionChosen;

        public event TypedEventHandler<AutoSuggestBox, AutoSuggestBoxQuerySubmittedEventArgs> QuerySubmitted;

        public event TypedEventHandler<TokenListBox, object> TokenItemAdded;

        public event TypedEventHandler<TokenListBox, object> TokenItemClicked;
    }
}
