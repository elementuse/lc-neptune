using Neptune.Yibao.{{cookiecutter.channel_name}}.Model;
using System;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    public class YibaoTestService : IYibaoService
    {
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReadCardResponse ReadCard(ReadCardRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PreSettleResponse PreSettle(PreSettleRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SettleResponse Settle(SettleRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefundResponse Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
