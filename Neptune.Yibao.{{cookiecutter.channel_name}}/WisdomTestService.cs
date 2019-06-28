using Neptune.Yibao.{{cookiecutter.channel_name }}.Model;
using System;
using System.Collections.Generic;

namespace Neptune.Yibao.{{cookiecutter.channel_name }}
{
    public class WisdomTestService : IWisdomService
    {
        /// <summary>
        /// 接诊提示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RemindResponse Remind(RemindRequest request)
        {
            throw NotImplementedException();
        }

        /// <summary>
        /// 门诊审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WisdomResponse Audit(WisdomRequest<Audit> request)
        {
            throw NotImplementedException();
        }
    }
}