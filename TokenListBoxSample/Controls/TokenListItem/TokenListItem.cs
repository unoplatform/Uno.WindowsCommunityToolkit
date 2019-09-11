using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TokenListBoxSample.Controls
{
    [TemplatePart(Name = PART_ClearButton, Type = typeof(Button))]
    public partial class TokenListItem : Button
    {
        private const string PART_ClearButton = "PART_ClearButton";

        private Button _clearButton;

        public event TypedEventHandler<TokenListItem, RoutedEventArgs> ClearClicked;

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(TokenListItem),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsSelectedChanged)));

        public static readonly DependencyProperty ClearButtonStyleProperty = DependencyProperty.Register(
            nameof(ClearButtonStyle),
            typeof(Style),
            typeof(TokenListItem),
            new PropertyMetadata(Visibility.Collapsed));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public Style ClearButtonStyle
        {
            get => (Style)GetValue(ClearButtonStyleProperty);
            set => SetValue(ClearButtonStyleProperty, value);
        }

        public TokenListItem()
        {
            DefaultStyleKey = typeof(TokenListItem);

            PointerEntered += TokenListItem_PointerEntered;
            PointerExited += TokenListItem_PointerExited;
            RightTapped += TokenListItem_RightTapped;
            KeyDown += TokenListItem_KeyDown;
        }

        private void TokenListItem_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Back:
                case Windows.System.VirtualKey.Delete:
                    ClearButton_Click(sender, e);
                    break;
            }
        }

        private void TokenListItem_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            ContextFlyout.ShowAt(this);
        }

        private void TokenListItem_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            UpdateVisualState();
        }

        private void TokenListItem_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            UpdateVisualState();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_clearButton != null)
            {
                _clearButton.Click -= ClearButton_Click;
            }

            _clearButton = (Button)GetTemplateChild(PART_ClearButton);

            if (_clearButton != null)
            {
                _clearButton.Click += ClearButton_Click;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearClicked?.Invoke(this, e);
        }

        private void UpdateVisualState(bool useTransitions = true)
        {
            var stateName = IsPointerOver
                ? IsSelected ? "PointerOverSelected" : "PointerOver"
                : IsSelected ? "Selected" : "Normal";

            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TokenListItem item)
            {
                item.UpdateVisualState();
            }
        }
    }
}
