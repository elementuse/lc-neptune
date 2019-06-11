using System.ComponentModel.DataAnnotations;

namespace Neptune.Yibao.{{channel_name}}
{
    public class YibaoSetting
    {
        [Display(Name = "启用测试")]
        [UIHint("Checkbox")]
        public bool IsTest { get; set; } = true;
    }
}
