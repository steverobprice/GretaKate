using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mustache;
using System.IO;

namespace GretaKate.Services
{
    public interface ITemplateService
    {
        string Load(string templateName);
        string Parse(string template, object data);
    }

    public class TemplateService : ITemplateService
    {
        public string Load(string templateFilePath)
        {
            var template = string.Empty;

            if (File.Exists(templateFilePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(templateFilePath))
                    {
                        template = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    // TODO: exception
                }
            }

            return template;
        }

        public string Parse(string template, object data)
        {
            string result = string.Empty;

            try
            {
                FormatCompiler compiler = new FormatCompiler();
                Generator generator = compiler.Compile(template);
                result = generator.Render(data);
            }
            catch (Exception ex)
            {
                // TODO: log error
            }

            return result;
        }

    }
}
