using System.Runtime.InteropServices;
using System.Text;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    public class YibaoApi
    {
        [DllImport("mhs.dll")]
        public static extern int f_UserBargaingInit(string Data1, StringBuilder retMsg, string Data2);

        [DllImport("mhs.dll")]
        public static extern int f_UserBargaingClose(string Data1, StringBuilder retMsg, string Data2);

        [DllImport("mhs.dll")]
        public static extern int f_UserBargaingApply(int Code, double No, string Data1, StringBuilder retMsg, string Data2);
    }
}
