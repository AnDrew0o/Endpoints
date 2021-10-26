namespace endpoints {
    public class Pluralisation
    {
        public static string GetForm(int num, string a, string b, string c)
        {
            int temp = num % 100;
            if (11 <= temp && temp <= 14)
            {
                return c;
            }
            else
            {
                temp = temp % 10;
                if (temp == 1)
                {
                    return a;
                }
                else if (2 <= temp && temp <= 4)
                {
                    return b;
                }
                else
                {
                    return c;
                }
            }
        }
    }
}