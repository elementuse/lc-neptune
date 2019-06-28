using Neptune.Yibao.{{cookiecutter.channel_name}}.Model;
using System;
using System.Text;
using System.Threading;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    public class YibaoService : IYibaoService
    {
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ReadCardResponse ReadCard(ReadCardRequest request)
        {
            return SendRequest<ReadCardRequest, ReadCardResponse>(111, request);
        }

        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PreSettleResponse PreSettle(PreSettleRequest request)
        {
            return SendRequest<PreSettleRequest, PreSettleResponse>(222, request);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SettleResponse Settle(SettleRequest request)
        {
            return SendRequest<SettleRequest, SettleResponse>(333, request);
        }

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefundResponse Refund(RefundRequest request)
        {
            return SendRequest<RefundRequest, RefundResponse>(444, request);
        }

        #region 接口调用

        /// <summary>
        /// 初始化环境
        /// </summary>
        public static void Init(string hospitalCode)
        {
            var retMsg = new StringBuilder(2048);
            int flag = YibaoApi.f_UserBargaingInit("", retMsg, hospitalCode);

            if (flag != 0)
            {
                throw new Exception($"医保环境初始化: {retMsg}");
            }
        }

        public static bool isInit = false;
        private static AutoResetEvent waitHandler = new AutoResetEvent(false);
        public static TResponse SendRequest<TRequest, TResponse>(int code, TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase, new()
        {
            PluginEntry.Context.Logger.Info($"调用接口:【{code}】");

            var response = new TResponse();

            try
            {
                if (string.IsNullOrEmpty(request.HospitalCode))
                {
                    throw new Exception("医疗机构编号不能为空！");
                }

                if (!isInit && !PluginEntry.YibaoSetting.IsTest)
                {
                    PluginEntry.Context.Logger.Info($"接口初始化");
                    Init(request.HospitalCode);
                    isInit = true;
                }

                string inputData = MessageSerializer.SerializeRequest(request);

                PluginEntry.Context.Logger.Info($"【{code}】输入: {inputData}");
                StringBuilder retMsg = new StringBuilder(4096);
                waitHandler.WaitOne(3000);
                int result = YibaoApi.f_UserBargaingApply(code, 0, inputData, retMsg, request.HospitalCode);
                waitHandler.Set();
                string value = retMsg.ToString().Trim();
                PluginEntry.Context.Logger.Info($"【{code}】返回值: {result} 输出: {value}");

                if (result == 0)
                {
                    response = MessageSerializer.Deserialize<TResponse>(value);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResultMessage = "交易失败";
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ResultMessage = ex.Message;
                PluginEntry.Context.Logger.Error($"【{code}】错误", ex);
            }

            return response;
        }

        #endregion
    }
}
