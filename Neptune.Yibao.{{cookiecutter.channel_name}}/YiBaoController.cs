using Neptune.Contract.Components.Web;
using Neptune.Yibao.{{cookiecutter.channel_name}}.Model;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    public class YiBaoController : WebComponent, IYibaoService
    {
        static YiBaoController()
        {
            CreateService(PluginEntry.YibaoSetting);
        }

        private static IYibaoService _service;
        public static void CreateService(YibaoSetting setting)
        {
            if (setting.IsTest)
            {
                _service = new YibaoTestService();
            }
            else
            {
                _service = new YibaoService();
            }
        }

        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReadCardResponse ReadCard(ReadCardRequest request)
        {
            return _service.ReadCard(request);
        }

        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PreSettleResponse PreSettle(PreSettleRequest request)
        {
            return _service.PreSettle(request);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SettleResponse Settle(SettleRequest request)
        {
            return _service.Settle(request);
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefundResponse Refund(RefundRequest request)
        {
            return _service.Refund(request);
        }
    }
}
