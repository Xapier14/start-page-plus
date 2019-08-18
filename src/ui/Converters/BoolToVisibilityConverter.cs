﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace StartPagePlus.UI.Converters
{
    //http://putridparrot.com/blog/markupextension/
    //https://www.broculos.net/2014/04/wpf-how-to-use-converters-without.html

    //usage:
    //<Button Content = "Cancel" Visibility="{Binding IsCancelVisible, Converter={FolderViewer:BooleanToVisibilityConverter WhenFalse = Collapsed}}">

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : ConverterMarkupExtension
    {
        public BoolToVisibilityConverter() : this(Visibility.Collapsed)
        { }

        public BoolToVisibilityConverter(Visibility whenFalse)
            => WhenFalse = whenFalse;

        [ConstructorArgument("WhenFalse")]
        public Visibility WhenFalse { get; set; }

        public override object Convert(object value, Type targetType, object parameter,
                         CultureInfo culture)
        {
            var result = (value is bool?)
                ? ((bool?)value).GetValueOrDefault(false)
                : false;
            if (value is bool)
            {
                result = (bool)value;
            }
            return result
                ? Visibility.Visible
                : WhenFalse;
        }

        public override object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture) => (value is Visibility) ?
                      (Visibility)value == Visibility.Visible : false;

        //public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    => (value != null) && (value is bool) && (bool)value
        //        ? Visibility.Visible
        //        : (object)Visibility.Collapsed;

        //public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    => throw new NotImplementedException();
    }

    //[ValueConversion(typeof(bool), typeof(Visibility))]
    //public class BoolToVisibilityConverter : IValueConverter
    //{
    //    private bool isReversed;
    //    private bool useHidden;

    //    public BoolToVisibilityConverter()
    //    { }

    //    public BoolToVisibilityConverter(bool isReversed, bool useHidden)
    //    {
    //        this.isReversed = isReversed;
    //        this.useHidden = useHidden;
    //    }

    //    public bool IsReversed
    //    {
    //        get => isReversed;
    //        set => isReversed = value;
    //    }

    //    public bool UseHidden
    //    {
    //        get => useHidden;
    //        set => useHidden = value;
    //    }

    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);

    //        if (IsReversed)
    //        {
    //            val = !val;
    //        }

    //        if (val)
    //        {
    //            return Visibility.Visible;
    //        }

    //        return UseHidden
    //            ? Visibility.Hidden
    //            : Visibility.Collapsed;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (!(value is Visibility))
    //        {
    //            return DependencyProperty.UnsetValue;
    //        }

    //        var visibility = (Visibility)value;
    //        var result = visibility == Visibility.Visible;

    //        if (IsReversed)
    //        {
    //            result = !result;
    //        }

    //        return result;
    //    }
    //}
}