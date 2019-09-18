using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Uwp.SampleApp.Models;
using Windows.UI.Xaml;

namespace Microsoft.Toolkit.Uwp.SampleApp.Shared.Triggers
{
    public class PaneStateTrigger : StateTriggerBase
	{
        #region Property: Binding
        public static readonly DependencyProperty BindingProperty = DependencyProperty.Register(
            nameof(Binding),
            typeof(PaneState),
            typeof(PaneStateTrigger),
            new PropertyMetadata(default(PaneState), (s, e) => (s as PaneStateTrigger)?.UpdateTrigger()));

        public PaneState Binding
        {
            get => (PaneState)GetValue(BindingProperty);
            set => SetValue(BindingProperty, value);
        }
        #endregion

        #region Property: Value
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(PaneState),
            typeof(PaneStateTrigger),
            new PropertyMetadata(default(PaneState), (s, e) => (s as PaneStateTrigger)?.UpdateTrigger()));

        public PaneState Value
        {
            get => (PaneState)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        private void UpdateTrigger() => SetActive(Binding == Value);
    }
}
