using Neptune.Contract.Components.Web;
using Neptune.Yibao.{{cookiecutter.channel_name}}.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
