namespace Neptune.Yibao.{{cookiecutter.channel_name}}.Model
{
    public class ResponseBase
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回值信息
        /// </summary>
        public string ResultMessage { get; set; }
    }
}
