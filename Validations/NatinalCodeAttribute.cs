using System.ComponentModel.DataAnnotations;

namespace WoundClinic.Validations
{
    public class NationalCodeAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string Number = value.ToString();
            if ((Number.Length < 8) || (Number.Length > 10)) return false;
            else if (Number.Length == 8) Number = "00" + Number;
            else if (Number.Length == 9) Number = "0" + Number;

            //رقم‌های کد ملی نباید یکسان باشد
            string[] allDigitEqual = new string[10] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(Number)) return false;

            //عملیات تشخیص صحت
            var chArray = Number.ToCharArray();
            var num1 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var ControlCode = Convert.ToInt32(chArray[9].ToString());

            var remain = (num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9) % 11;
            if ((remain < 2) && (ControlCode == remain)) return true;
            if ((remain >= 2) && ((11 - remain) == ControlCode)) return true;
            return false;
        }
    }
}
