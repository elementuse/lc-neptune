using Neptune.Yibao.{{cookiecutter.channel_name }}.Model;

namespace Neptune.Yibao.{{cookiecutter.channel_name }}
{
    public interface IWisdomService
    {
        /// <summary>
        /// 接诊提示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RemindResponse Remind(RemindRequest request);

        /// <summary>
        /// 门诊审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AuditResponse Audit(AuditRequest request);
    }
}
