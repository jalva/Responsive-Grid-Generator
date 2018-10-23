using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppWithGridGenerator.Models;
using WebAppWithGridGenerator.ViewModels;

namespace WebAppWithGridGenerator.ViewComponents.GridContainer
{
    public class GridContainerViewComponent : ViewComponent
    {
        private IViewRenderService _viewRenderService;

        public GridContainerViewComponent(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public IViewComponentResult Invoke(GridLayoutContainer_ViewModel model)
        {
            model.ViewRenderService = _viewRenderService;
            return View(model);
        }
    }
}
