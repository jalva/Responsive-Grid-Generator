using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ResponsiveLayoutGenerator
{
    public class ResponsiveLayoutRenderer
    {
        public ResponsiveLayoutRenderer(string lgRowDefs, string mdRowDefs, string smRowDefs, string xsRowDefs)
        {
            cssHelper = new ResponsiveClassesHelper(lgRowDefs, mdRowDefs, smRowDefs, xsRowDefs);
        }

        private ResponsiveClassesHelper cssHelper { get; set; }

        public HtmlString RenderColumnsGrid(string customColumnCssClasses, List<string> columnContentHtml)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < cssHelper.TotalColumns; i++)
            {
                var clearfixHtml = string.Format(@"<div class='clearfix {0}'></div>", cssHelper.GetClearfixCssClasses(i));

                sb.Append(clearfixHtml);

                var colHtml = string.Format(@"<div class='{0} {1}'>{2}</div>", customColumnCssClasses, cssHelper.GetColumnCssClasses(i), columnContentHtml[i]);

                sb.Append(colHtml);
            }

            return new HtmlString(sb.ToString());
        }


        public static string GetString(IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
