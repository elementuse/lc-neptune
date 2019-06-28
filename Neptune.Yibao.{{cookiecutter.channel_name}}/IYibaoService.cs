using Neptune.Yibao.{{cookiecutter.channel_name}}.Model;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    public interface IYibaoService
    {
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ReadCardResponse ReadCard(ReadCardRequest request);

        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PreSettleResponse PreSettle(PreSettleRequest request);

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SettleResponse Settle(SettleRequest request);

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RefundResponse Refund(RefundRequest request);
    }
}
