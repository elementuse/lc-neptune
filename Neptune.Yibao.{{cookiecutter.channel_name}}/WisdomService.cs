using Neptune.Log;
using Neptune.Yibao.{{cookiecutter.channel_name }}.Model;
using System;
using System.IO;
using System.Management;
using System.Text;
using System.Threading;

namespace Neptune.Yibao.{{cookiecutter.channel_name }}
{
    public class WisdomService : IWisdomService
    {
        /// <summary>
        /// 接诊提示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RemindResponse Remind(RemindResponse request)
        {
            return SendRequest<RemindRequest, RemindResponse>(111, request);
        }

        /// <summary>
        /// 门诊审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AuditResponse Audit(AuditRequest request)
        {
            return SendRequest<AuditRequest, AuditResponse>(222, request);
        }

        #region 接口调用

        private static AutoResetEvent waitHandler = new AutoResetEvent(false);
        public static TResponse SendRequest<TRequest, TResponse>(int code, TRequest request)
                where TRequest : WisdomRequest
                where TResponse : WisdomResponse, new()
        {
            PluginEntry.Context.Logger.Info($"审核接口:【{code}】");

            var response = new TResponse();

            try
            {
                if (string.IsNullOrEmpty(request.HospitalCode))
                {
                    throw new Exception("医疗机构编号不能为空！");
                }

                //input data
                string inputData="";

                PluginEntry.Context.Logger.Info($"【{code}】输入: {inputData}");
                waitHandler.WaitOne(3000);
                //string result = YibaoApi.Audit4HospitalPortal(inputData);
                string result = "";
                waitHandler.Set();
                PluginEntry.Context.Logger.Info($"【{code}】输出: {result}");

                //process result
            }
            catch (Exception ex)
            {
                //process result
                PluginEntry.Context.Logger.Error($"【{code}】错误", ex);
            }

            return response;
        }

        #endregion
    }
}
