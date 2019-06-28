using Neptune.Contract.Components.Setting;
using Neptune.Contract.Components.Web;
using Neptune.Contract.Plugins;
using System.ComponentModel.Composition;

namespace Neptune.Yibao.{{cookiecutter.channel_name}}
{
    [Export(typeof(IPluginEntry))]
    public class PluginEntry : PluginEntryBase<PluginEntry>
    {
        public override void Start(PluginContext pluginContext)
        {
            Context = pluginContext;
            YibaoSetting = pluginContext.GetSetting<YibaoSetting>();

            // 注册 webapi
            pluginContext.Container.Resolve<IWebComponentService>().Register<YiBaoController>(pluginContext);
            pluginContext.Container.Resolve<IWebComponentService>().Register<WisdomController>(pluginContext);

            // 注册 配置
            pluginContext.Container.Resolve<IUserSettingComponentService>().RegisterSetting<YibaoSetting>(pluginContext,
            (o) =>
            {
                var setting = (YibaoSetting)o;
                YibaoSetting = setting;
                YiBaoController.CreateService(setting);
                Context.Logger.Info($"{{cookiecutter.display_name}}医保配置更新: " + Newtonsoft.Json.JsonConvert.SerializeObject(setting));
            });
        }

        public static YibaoSetting YibaoSetting { get; set; }
    }
}
