using System.Windows.Data;
using System.Windows.Media;

namespace PeopleViewer.Desktop.Ninject;

public class DecadeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int year = ((DateTime)value).Year;
        return string.Format("{0}0s", year.ToString()[..3]);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class RatingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int rating = (int)value;
        return string.Format("{0}/10 Stars", rating.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class RatingStarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int rating = (int)value;
        string output = string.Empty;
        return output.PadLeft(rating, '*');
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string input = (string)value;
        //int rating = 0;

        //foreach (var ch in input)
        //    if (ch == '*')
        //        rating++;

        //return rating;

        return input.Count(c => c == '*');
    }
}

public class DecadeBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int decade = (((DateTime)value).Year / 10) * 10;

        return decade switch
        {
            1970 => new SolidColorBrush(Colors.Maroon),
            1980 => new SolidColorBrush(Colors.DarkGreen),
            1990 => new SolidColorBrush(Colors.DarkSlateBlue),
            2000 => new SolidColorBrush(Colors.CadetBlue),
            _ => new SolidColorBrush(Colors.DarkSlateGray),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
