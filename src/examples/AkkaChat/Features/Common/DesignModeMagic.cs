using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace AkkaChat.Features.Common
{
    public static class DesignModeMagic
    {
        public static T GetValueForXBind<T>(
            this DependencyObject dObj,
            DependencyProperty property, T field)
        {
            if (DesignMode.DesignModeEnabled)
            {
                return (T) dObj.GetValue(property);
            }
            return field;
        }

        public static bool SetValueForXBind<T>(
            this DependencyObject dObj,
            DependencyProperty property,
            ref T field,
            T value)

        {
            var equals = Equals(field, value);
            if (!equals)
            {
                field = value;
                if (DesignMode.DesignModeEnabled)
                {
                    dObj.SetValue(property, value);
                }
            }
            return !equals;
        }
    }
}