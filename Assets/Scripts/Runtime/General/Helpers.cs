using System.Text;

public static class Helpers
{
    public static string ConverCashToText(this int cash)
    {
        StringBuilder strb = new StringBuilder();

        if (cash > 999999999)
        {
            uint tmp = (uint)(cash / 100000000);
            short dec = (short)(tmp % 10);

            strb.Append(tmp / 10);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('B');
        }
        else if (cash > 999999)
        {
            uint tmp = (uint)(cash / 100000L);
            short dec = (short)(tmp % 10);

            strb.Append(tmp / 10);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('M');
        }
        else if (cash > 999)
        {
            uint tmp = (uint)(cash / 100L);
            short dec = (short)(tmp % 10);

            strb.Append(tmp / 10);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('K');
        }
        else
        {
            strb.Append(cash);
        }

        return strb.ToString();
    }

    public static string ConverCashToText(this ulong cash)
    {
        StringBuilder strb = new StringBuilder();

        if (cash > 999999999999999L)
        {
            uint tmp = (uint)(cash / 100000000000000);
            short dec = (short)(tmp % 10L);

            strb.Append(tmp / 10L);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('Q');
        }
        else if (cash > 999999999999L)
        {
            uint tmp = (uint)(cash / 100000000000L);
            short dec = (short)(tmp % 10L);

            strb.Append(tmp / 10L);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('T');
        }
        else if (cash > 999999999L)
        {
            uint tmp = (uint)(cash / 100000000L);
            short dec = (short)(tmp % 10L);

            strb.Append(tmp / 10L);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('B');
        }
        else if (cash > 999999L)
        {
            uint tmp = (uint)(cash / 100000L);
            short dec = (short)(tmp % 10L);

            strb.Append(tmp / 10L);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('M');
        }
        else if (cash > 999L)
        {
            uint tmp = (uint)(cash / 100L);
            short dec = (short)(tmp % 10L);

            strb.Append(tmp / 10L);
            if (dec > 0)
                strb.AppendFormat(".{0}", dec);

            strb.Append('K');
        }
        else
        {
            strb.Append(cash);
        }

        return strb.ToString();
    }
}
