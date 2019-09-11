using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TokenListBoxSample.Controls
{
    [TemplatePart(Name = PART_AutoSuggestBox, Type = typeof(AutoSuggestBox))]
    [TemplatePart(Name = PART_WrapPanel, Type = typeof(WrapPanel))]
    public partial class TokenListBox : Control
    {
        private const string PART_AutoSuggestBox = "PART_AutoSuggestBox";
        private const string PART_WrapPanel = "PART_WrapPanel";

        private AutoSuggestBox _autoSuggestBox;
        private WrapPanel _wrapPanel;

        public TokenListBox()
        {
            DefaultStyleKey = typeof(TokenListBox);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_autoSuggestBox != null)
            {
                _autoSuggestBox.QuerySubmitted -= AutoSuggestBox_QuerySubmitted;
                _autoSuggestBox.SuggestionChosen -= AutoSuggestBox_SuggestionChosen;
                _autoSuggestBox.TextChanged -= AutoSuggestBox_TextChanged;
                _autoSuggestBox.KeyDown -= AutoSuggestBox_KeyDown;
                _autoSuggestBox.GettingFocus -= AutoSuggestBox_GettingFocus;
                _autoSuggestBox.LosingFocus -= AutoSuggestBox_LosingFocus;
                _autoSuggestBox.CharacterReceived -= AutoSuggestBox_CharacterReceived;
            }

            _autoSuggestBox = (AutoSuggestBox)GetTemplateChild(PART_AutoSuggestBox);
            _wrapPanel = (WrapPanel)GetTemplateChild(PART_WrapPanel);

            if (_autoSuggestBox != null)
            {
                _autoSuggestBox.QuerySubmitted += AutoSuggestBox_QuerySubmitted;
                _autoSuggestBox.SuggestionChosen += AutoSuggestBox_SuggestionChosen;
                _autoSuggestBox.TextChanged += AutoSuggestBox_TextChanged;
                _autoSuggestBox.KeyDown += AutoSuggestBox_KeyDown;
                _autoSuggestBox.GettingFocus += AutoSuggestBox_GettingFocus;
                _autoSuggestBox.LosingFocus += AutoSuggestBox_LosingFocus;
                _autoSuggestBox.CharacterReceived += AutoSuggestBox_CharacterReceived;
            }

            var selectAllMenuItem = new MenuFlyoutItem
            {
                Text = "Select All"
            };
            selectAllMenuItem.Click += (s, e) => SelectAll();
            var menuFlyout = new MenuFlyout();
            menuFlyout.Items.Add(selectAllMenuItem);
            ContextFlyout = menuFlyout;
        }

        private void AutoSuggestBox_CharacterReceived(UIElement sender, Windows.UI.Xaml.Input.CharacterReceivedRoutedEventArgs args)
        {
            if (args.Character.ToString() == TokenDelimiter && sender is AutoSuggestBox autoSuggestBox)
            {
                AddToken(autoSuggestBox.Text);
                autoSuggestBox.Text = string.Empty;
                autoSuggestBox.Focus(FocusState.Programmatic);
            }
        }

        private void AutoSuggestBox_LosingFocus(UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            if (sender is AutoSuggestBox autoSuggestBox)
            {
                autoSuggestBox.QueryIcon = QueryIcon as IconElement;
            }
        }

        private void AutoSuggestBox_GettingFocus(UIElement sender, Windows.UI.Xaml.Input.GettingFocusEventArgs args)
        {
            if (sender is AutoSuggestBox autoSuggestBox)
            {
                autoSuggestBox.QueryIcon = null;
            }
        }

        private void AutoSuggestBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            switch(e.Key)
            {
                case VirtualKey.Back:
                case VirtualKey.Delete:

                    if (_autoSuggestBox.Text != string.Empty || _wrapPanel.Children.Count <= 1)
                    {
                        break;
                    }

                    // The last item is the AutoSuggestBox. Get the second to last.
                    var lastTokenIndex = _wrapPanel.Children.Count - 2;
                    var lastToken = _wrapPanel.Children[lastTokenIndex];
                    RemoveToken(lastToken as TokenListItem);
                    break;

                case VirtualKey.Left:
                    break;

                case VirtualKey.Right:
                    break;

                case VirtualKey.Up:
                    // TODO: Move focus on last suggestion
                    break;

                case VirtualKey.Down:
                    // TODO: Move focus to 
                    break;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.QueryText != string.Empty)
            {
                AddToken(args.QueryText);
                sender.Text = string.Empty;
                sender.Focus(FocusState.Programmatic);
            }

            QuerySubmitted?.Invoke(sender, args);
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            AddToken(args.SelectedItem);
            sender.Text = string.Empty;
            sender.Focus(FocusState.Programmatic);
            SuggestionChosen?.Invoke(sender, args);
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            QueryTextChanged?.Invoke(sender, args);
        }

        private void TokenListItem_ClearClicked(TokenListItem sender, RoutedEventArgs args)
        {
            RemoveToken(sender);
        }

        private void TokenListItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is TokenListItem item)
            {
                if (!item.IsSelected && !CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down))
                {
                    foreach (var child in _wrapPanel.Children)
                    {
                        if (child is TokenListItem childItem)
                        {
                            childItem.IsSelected = false;
                        }
                    }
                }

                item.IsSelected = !item.IsSelected;

                var clickedItem = item.Content;
                TokenItemClicked?.Invoke(this, clickedItem);
            }
        }

        private void AddToken(object data)
        {
            var item = new TokenListItem()
            {
                Content = data,
                ContentTemplateSelector = TokenItemTemplateSelector,
                ContentTemplate = TokenItemTemplate,
                Style = TokenItemStyle
            };
            item.Click += TokenListItem_Click;
            item.ClearClicked += TokenListItem_ClearClicked;

            var removeMenuItem = new MenuFlyoutItem
            {
                Text = "Remove"
            };
            removeMenuItem.Click += (s, e) => RemoveToken(item);
            var menuFlyout = new MenuFlyout();
            menuFlyout.Items.Add(removeMenuItem);
            item.ContextFlyout = menuFlyout;

            var i = _wrapPanel.Children.Count - 1;
            _wrapPanel.Children.Insert(i, item);

            TokenItemAdded?.Invoke(this, data);
        }

        private void RemoveToken(TokenListItem item)
        {
            var itemIndex = Math.Max(_wrapPanel.Children.IndexOf(item) - 1, 0);
            _wrapPanel.Children.Remove(item);

            if (_wrapPanel.Children[itemIndex] is Control control)
            {
                control.Focus(FocusState.Programmatic);
            }
        }

        private void SelectAll()
        {
            foreach (var child in _wrapPanel.Children)
            {
                if (child is TokenListItem item)
                {
                    item.IsSelected = true;
                }
            }
        }

        public string GetUntokenizedText()
        {
            var tokenStrings = new List<string>();
            foreach(var child in _wrapPanel.Children)
            {
                if (child is TokenListItem item)
                {
                    tokenStrings.Add(item.Content.ToString());
                }
            }
            return string.Join(TokenDelimiter, tokenStrings);
        }
    }
}
