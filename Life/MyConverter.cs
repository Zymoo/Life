using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Life
{
    public class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value)
                {
                    return "Running";
                }
            }
            return "Running";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if (((string)value).Equals("Running"))
                    return false;
                else
                    return true;
            }
            return true;
        }
    }
}
