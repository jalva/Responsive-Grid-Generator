using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAppWithGridGenerator.Models;

namespace WebAppWithGridGenerator.ViewModels
{
    public class GridLayoutContainer_ViewModel
    {
        public IViewRenderService ViewRenderService { get; set; }
        public List<Tuple<string, object>> ColumnPartialsWithModels { get; set; }
        public string Lg_Row_Indx_To_Col_Sizes_Csv { get; set; }
        public string Md_Row_Indx_To_Col_Sizes_Csv { get; set; }
        public string Sm_Row_Indx_To_Col_Sizes_Csv { get; set; }
        public string Xs_Row_Indx_To_Col_Sizes_Csv { get; set; }
        

        public string CssClasses { get; set; }
        public string ColumnCssClasses { get; set; }

        public string SeeMoreAjaxEndpointUrl { get; set; }
        public int SeeMoreAjaxPageSize { get; set; }
        public string SeeMoreAjaxPaginationLabel { get; set; }
        public bool ShowSeeMoreAjaxButton { get; set; }

        public string CurrentPageId { get; set; }
        public bool IsAjaxPaginationRequest { get; set; }
        public int TotalItemCount { get; set; }
        

    }
}
