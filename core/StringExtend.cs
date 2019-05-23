namespace MVC_DotNet.core
{
    public static class StringExtend
    {
        public static int ToInt (this string s)
        {
            int ret = 0;
            int.TryParse(s, out ret); 
            return ret;
        }
    }
}