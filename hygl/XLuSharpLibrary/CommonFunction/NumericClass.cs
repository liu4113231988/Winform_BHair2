using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XLuSharpLibrary.CommonFunction
{
    public class NumericClass
    {
        public static bool IsNumber(string strtext)
        {
            Regex regex = new Regex("[^0-9.-]");
            Regex regex2 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex regex3 = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            string text = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string text2 = "^([-]|[0-9])[0-9]*$";
            Regex regex4 = new Regex(string.Concat(new string[]
            {
                "(",
                text,
                ")|(",
                text2,
                ")"
            }));
            return !regex.IsMatch(strtext) && !regex2.IsMatch(strtext) && !regex3.IsMatch(strtext) && regex4.IsMatch(strtext);
        }

        public static void NumberAccpter(object sender, KeyPressEventArgs e, bool point)
        {
            int keyChar = (int)e.KeyChar;
            if ((keyChar >= 48 && keyChar <= 57) || keyChar == 8 || keyChar == 46)
            {
                if (!point && keyChar == 46)
                {
                    e.Handled = true;
                }
                else
                {
                    string text = "";
                    if (sender != null && sender is TextBox)
                    {
                        text = ((TextBox)sender).Text;
                    }
                    if (text == "" && keyChar == 46)
                    {
                        e.Handled = true;
                    }
                    else if (text.IndexOf(".") >= 0 && keyChar == 46)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
