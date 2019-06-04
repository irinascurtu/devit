using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SuperHeroes.Domain;

namespace SuperHeroes.OutputFormatters
{
    #region classdef
    public class AwesomeOutputFormatter : TextOutputFormatter
    #endregion
    {
        #region ctor
        public AwesomeOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/awesome+vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        #endregion

        #region canwritetype
        protected override bool CanWriteType(Type type)
        {
            if (typeof(Contact).IsAssignableFrom(type)
                || typeof(IEnumerable<SuperHero>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        #endregion

        #region writeresponse
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<SuperHero>)
            {
                foreach (SuperHero hero in context.Object as IEnumerable<SuperHero>)
                {
                    FormatVcard(buffer, hero);
                }
            }
            else
            {
                var hero = context.Object as SuperHero;
                FormatVcard(buffer, hero);
            }
            return response.WriteAsync(buffer.ToString());
        }

        private static void FormatVcard(StringBuilder buffer, SuperHero hero)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendFormat($"N:{hero.LastName};{hero.FirstName}\r\n");
            buffer.AppendFormat($"FN:{hero.FirstName} {hero.LastName}\r\n");
            buffer.AppendFormat($"UID:{hero.Id}\r\n");
            buffer.AppendLine("END:VCARD");

        }
        #endregion
    }

}
