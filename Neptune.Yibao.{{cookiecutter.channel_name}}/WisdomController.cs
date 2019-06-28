using Neptune.Contract.Components.Web;
using Neptune.Yibao.{{cookiecutter.channel_name }}.Model;

namespace Neptune.Yibao.{{cookiecutter.channel_name }}
{
    public class WisdomController : WebComponent, IWisdomService
    {
        static WisdomController()
        {
            CreateService(PluginEntry.YibaoSetting);
        }

        private static IWisdomService _service;
        public static void CreateService(YibaoSetting setting)
        {
            if (setting.IsTest)
            {
                _service = new WisdomTestService();
            }
            else
            {
                _service = new WisdomService();
            }
        }

        /// <summary>
        /// 接诊提示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RemindResponse Remind(RemindRequest request)
        {
            return _service.Remind(request);
        }

        /// <summary>
        /// 门诊审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AuditResponse Audit(AuditRequest request)
        {
            return _service.Audit(request);
        }
    }
}