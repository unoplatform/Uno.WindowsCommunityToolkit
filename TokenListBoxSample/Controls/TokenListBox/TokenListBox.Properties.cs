using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TokenListBoxSample.Controls
{
    public partial class TokenListBox : Control
    {
        public static readonly DependencyProperty AutoSuggestBoxStyleProperty = DependencyProperty.Register(
            nameof(AutoSuggestBoxStyle),
            typeof(Style),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register(
            nameof(DisplayMemberPath),
            typeof(string),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TextMemberPathProperty = DependencyProperty.Register(
            nameof(TextMemberPath),
            typeof(string),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TokenItemTemplateProperty = DependencyProperty.Register(
            nameof(TokenItemTemplate),
            typeof(DataTemplate),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TokenItemTemplateSelectorProperty = DependencyProperty.Register(
            nameof(TokenItemTemplateSelector),
            typeof(DataTemplateSelector),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TokenItemStyleProperty = DependencyProperty.Register(
            nameof(TokenItemStyle),
            typeof(Style),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TokenDelimiterProperty = DependencyProperty.Register(
            nameof(TokenDelimiter),
            typeof(string),
            typeof(TokenListBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TokenSpacingProperty = DependencyProperty.Register(
            nameof(TokenSpacing),
            typeof(double),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
            nameof(PlaceholderText),
            typeof(string),
            typeof(TokenListBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty QueryIconProperty = DependencyProperty.Register(
            nameof(QueryIcon),
            typeof(object),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty QueryTextProperty = DependencyProperty.Register(
            nameof(QueryText),
            typeof(string),
            typeof(TokenListBox),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SuggestedItemsSourceProperty = DependencyProperty.Register(
            nameof(SuggestedItemsSource),
            typeof(object),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SuggestedItemTemplateProperty = DependencyProperty.Register(
            nameof(SuggestedItemTemplate),
            typeof(DataTemplate),
            typeof(TokenListBox),
            new PropertyMetadata(null));

        public Style AutoSuggestBoxStyle
        {
            get => (Style)GetValue(AutoSuggestBoxStyleProperty);
            set => SetValue(AutoSuggestBoxStyleProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public string TextMemberPath
        {
            get => (string)GetValue(TextMemberPathProperty);
            set => SetValue(TextMemberPathProperty, value);
        }

        public DataTemplate TokenItemTemplate
        {
            get => (DataTemplate)GetValue(TokenItemTemplateProperty);
            set => SetValue(TokenItemTemplateProperty, value);
        }

        public DataTemplateSelector TokenItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TokenItemTemplateSelectorProperty);
            set => SetValue(TokenItemTemplateSelectorProperty, value);
        }

        public Style TokenItemStyle
        {
            get => (Style)GetValue(TokenItemStyleProperty);
            set => SetValue(TokenItemStyleProperty, value);
        }

        public string TokenDelimiter
        {
            get => (string)GetValue(TokenDelimiterProperty);
            set => SetValue(TokenDelimiterProperty, value);
        }

        public double TokenSpacing
        {
            get => (double)GetValue(TokenSpacingProperty);
            set => SetValue(TokenSpacingProperty, value);
        }

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public object QueryIcon
        {
            get => GetValue(QueryIconProperty);
            set
            {
                // Special case for parsing Symbol enum strings
                if (value is string valueString && Enum.TryParse(valueString, out Symbol symbol))
                {
                    SetValue(QueryIconProperty, new SymbolIcon(symbol));
                }
                else
                {
                    SetValue(QueryIconProperty, value);
                }
            }
        }

        public string QueryText
        {
            get => (string)GetValue(QueryTextProperty);
            set => SetValue(QueryTextProperty, value);
        }

        public object SuggestedItemsSource
        {
            get => GetValue(SuggestedItemsSourceProperty);
            set => SetValue(SuggestedItemsSourceProperty, value);
        }

        public object SuggestedItemTemplate
        {
            get => (DataTemplate)GetValue(SuggestedItemTemplateProperty);
            set => SetValue(SuggestedItemTemplateProperty, value);
        }
    }
}
