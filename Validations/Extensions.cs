namespace WoundClinic_WPF.Validations;

public static class Extensions
{
    public static DateTime ToMiladyDate(this string persianDate)
    {
        var date = persianDate.Split('/');

        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        return pc.ToDateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), 0, 0, 0, 0);
    }
    public static string ToPersianDate(this DateTime date)
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        return (pc.GetYear(date).ToString("0000") + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00"));
    }
    public static bool IsValidDate(this string date)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(date, @"^([1][3|4][0-9][0-9])[/]([0][1-9]|[1][012])[/]([0][1-9]|[1|2][0-9]|[3][01])$");
    }
}
//{
//    
//{
//    public enum PrescriptoinsType
//    {
//        پزشکی = 1,
//        دندانپزشکی = 2
//    }
//    public enum Monthes
//    {
//        فروردین = 1,
//        اردیبهشت = 2,
//        خرداد = 3,
//        تیر = 4,
//        مرداد = 5,
//        شهریور = 6,
//        مهر = 7,
//        آبان = 8,
//        آذر = 9,
//        دی = 10,
//        بهمن = 11,
//        اسفند = 12,
//    }
//    public static DataGridTextColumn AddColumn(this DataGrid datagrid, string Header, string binding)
//    {
//        var column = new DataGridTextColumn() { Header = Header };
//        column.Binding = new Binding(binding);
//        datagrid.Columns.Add(column);
//        return column;
//    }
//    public static DateTime ToMiladyDate(this string persianDate)
//    {
//        var date = persianDate.Split('/');

//        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
//        return pc.ToDateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), 0, 0, 0, 0);
//    }
//    public static string ToPersianDate(this DateTime date)
//    {
//        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
//        return (pc.GetYear(date).ToString("0000") + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00"));
//    }
//    public static bool IsValidDate(this string date)
//    {
//        return System.Text.RegularExpressions.Regex.IsMatch(date, @"^([1][3|4][0-9][0-9])[/]([0][1-9]|[1][012])[/]([0][1-9]|[1|2][0-9]|[3][01])$");
//    }
//    public static bool IsValidNationalNumber(this long NationalNumber)
//    {
//        string Number = NationalNumber.ToString();
//        if ((Number.Length < 8) || (Number.Length > 10)) return false;
//        else if (Number.Length == 8) Number = "00" + Number;
//        else if (Number.Length == 9) Number = "0" + Number;

//        //رقم‌های کد ملی نباید یکسان باشد
//        string[] allDigitEqual = new string[10] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
//        if (allDigitEqual.Contains(Number)) return false;

//        //عملیات تشخیص صحت
//        var chArray = Number.ToCharArray();
//        var num1 = Convert.ToInt32(chArray[0].ToString()) * 10;
//        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
//        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
//        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
//        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
//        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
//        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
//        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
//        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
//        var ControlCode = Convert.ToInt32(chArray[9].ToString());

//        var remain = (num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9) % 11;
//        if ((remain < 2) && (ControlCode == remain)) return true;
//        if ((remain >= 2) && ((11 - remain) == ControlCode)) return true;
//        return false;
//    }
//    public static string ToStringPhone(this long? phone) => (phone == null) ? null : ("0" + phone);
//    //public static bool IsValidPhone(this long? phone) => phone.IsMobile() || phone.IsPhone();
//    public static bool IsMobile(this long? mobile)
//    {
//        if (mobile == null) return false;
//        if (mobile.ToString().Length < 10) return false;
//        else return mobile.Value.ToString().StartsWith("9");
//    }
//    //public static bool IsPhone(this long? phone, byte preTell = 0)
//    //{
//    //    if (phone == null) return false;
//    //    if (phone.ToString().Length == 8) phone = (preTell.ToString() + phone.ToString()).Parse<long>();
//    //    if (phone.ToString().Length < 10) return false;
//    //    else return !phone.Value.ToString().StartsWith("9");
//    //}
//    public static void SetAnonymousPropertyValue(this object target, string PropertyName, object newValue)
//        => target.GetType().GetField("<" + PropertyName + ">i__Field", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, newValue);

//    public static string GetPropertyList(this object obj)
//    {
//        string properties = "";
//        var props = obj.GetType().GetProperties();
//        foreach (var p in props)
//        {
//            properties = string.Join(p.GetValue(obj, null).ToString(), ",");
//        }
//        return properties;
//    }
//    public static void EnterToTab(this Window win)
//    {
//        win.KeyDown += Win_KeyDown;
//    }

//    private static void Win_KeyDown(object sender, KeyEventArgs e)
//    {
//        if (e.Key == Key.Enter || e.Key == Key.Return)
//            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
//                (Keyboard.FocusedElement as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
//            else (Keyboard.FocusedElement as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
//    }
//}
//}
