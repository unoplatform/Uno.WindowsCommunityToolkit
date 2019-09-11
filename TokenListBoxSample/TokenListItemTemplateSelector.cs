using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TokenListBoxSample
{
    public class TokenItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DataItemTemplate { get; set; }
        public DataTemplate StringItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return (item is string) ? StringItemTemplate : DataItemTemplate;
        }
    }
}
