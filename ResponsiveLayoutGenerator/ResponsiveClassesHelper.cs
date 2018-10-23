using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiveLayoutGenerator
{
    class ResponsiveClassesHelper
    {
        public ResponsiveClassesHelper(string lgRowDefs, string mdRowDefs, string smRowDefs, string xsRowDefs)
        {
            lgDefs = QueryHelpers.ParseQuery(lgRowDefs);
            mdDefs = QueryHelpers.ParseQuery(mdRowDefs);
            smDefs = QueryHelpers.ParseQuery(smRowDefs);
            xsDefs = QueryHelpers.ParseQuery(xsRowDefs);

            lgRowColsDefs = lgDefs.Keys.SelectMany(k => lgDefs[k].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
            mdRowColsDefs = mdDefs.Keys.SelectMany(k => mdDefs[k].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
            smRowColsDefs = smDefs.Keys.SelectMany(k => smDefs[k].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
            xsRowColsDefs = xsDefs.Keys.SelectMany(k => xsDefs[k].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();

            lgClearFixDict = populateClearifxDict(lgDefs);
            mdClearFixDict = populateClearifxDict(mdDefs);
            smClearFixDict = populateClearifxDict(smDefs);
            xsClearFixDict = populateClearifxDict(xsDefs);

            TotalColumns = lgRowColsDefs.Count;
        }

        public string GetColumnCssClasses(int colIndex)
        {
            return string.Format("{0} {1} {2} {3}", lgColCss(colIndex), mdColCss(colIndex), smColCss(colIndex), xsColCss(colIndex));
        }

        public string GetClearfixCssClasses(int colIndex)
        {
            return string.Format("{0} {1} {2} {3}", lgClearFixCss(colIndex), mdClearFixCss(colIndex), smClearFixCss(colIndex), xsClearFixCss(colIndex));
        }

        public int TotalColumns { get; set; }

        private Dictionary<string, StringValues> lgDefs { get; set; }
        private Dictionary<string, StringValues> mdDefs { get; set; }
        private Dictionary<string, StringValues> smDefs { get; set; }
        private Dictionary<string, StringValues> xsDefs { get; set; }


        private List<string> lgRowColsDefs { get; set; }
        private List<string> mdRowColsDefs { get; set; }
        private List<string> smRowColsDefs { get; set; }
        private List<string> xsRowColsDefs { get; set; }


        private Dictionary<int, bool> lgClearFixDict { get; set; }
        private Dictionary<int, bool> mdClearFixDict { get; set; }
        private Dictionary<int, bool> smClearFixDict { get; set; }
        private Dictionary<int, bool> xsClearFixDict { get; set; }


        private static Dictionary<int, bool> populateClearifxDict(Dictionary<string, StringValues> rowDefs)
        {
            var clearFixDict = new Dictionary<int, bool>();
            var colToRowIndx = new Dictionary<int, int>();
            var currentColIndx = 0;
            for (var rowIndx = 0; rowIndx < rowDefs.Keys.Count; rowIndx++)
            {
                var columnsInRow = rowDefs[rowIndx.ToString()].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;

                for (var colIndx = 0; colIndx < columnsInRow; colIndx++)
                {
                    colToRowIndx.Add(currentColIndx, rowIndx);

                    var prevRowIndx = -1;
                    colToRowIndx.TryGetValue(currentColIndx - 1, out prevRowIndx);
                    // is this col in the new row compared from prev col?
                    clearFixDict.Add(currentColIndx, currentColIndx > 0 && rowIndx > prevRowIndx);
                    currentColIndx++;
                }
            }

            return clearFixDict;
        }

        private string lgColCss(int colIndex)
        {
            return "col-lg-" + lgRowColsDefs[colIndex];
        }
        private string mdColCss(int colIndex)
        {
            return "col-md-" + mdRowColsDefs[colIndex];
        }
        private string smColCss(int colIndex)
        {
            return "col-sm-" + smRowColsDefs[colIndex];
        }
        private string xsColCss(int colIndex)
        {
            return "col-xs-" + xsRowColsDefs[colIndex];
        }

        private string lgClearFixCss(int colIndex)
        {
            return lgClearFixDict[colIndex] ? "visible-lg-block" : "hidden-lg";
        }
        private string mdClearFixCss(int colIndex)
        {
            return mdClearFixDict[colIndex] ? "visible-md-block" : "hidden-md";
        }
        private string smClearFixCss(int colIndex)
        {
            return smClearFixDict[colIndex] ? "visible-sm-block" : "hidden-sm";
        }
        private string xsClearFixCss(int colIndex)
        {
            return xsClearFixDict[colIndex] ? "visible-xs-block" : "hidden-xs";
        }
    }
}
