using System.Runtime.InteropServices;
using System.Text;

namespace Neptune.Yibao.{{channel_name}}
{
    public class YibaoApi
    {
        [DllImport("JxdxHisJk.dll")]
        public static extern int f_UserBargaingInit(string UserID, string PassWD, StringBuilder retMsg);

        [DllImport("JxdxHisJk.dll")]
        public static extern int f_UserBargaingClose(StringBuilder retMsg);

        [DllImport("JxdxHisJk.dll")]
        public static extern int f_UserBargaingApply(string YWLX, string InDataP, StringBuilder OutData, StringBuilder retMsg);
    }
}
