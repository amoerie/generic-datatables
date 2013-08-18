using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenericDatatables.Core.Infrastructure.Attributes.JetBrains.Annotations;

namespace GenericDatatables.Web.Extensions
{
    public static class ExtensionsForControllerBase
    {
        /// <summary>
        ///     Renders a view to a string
        /// </summary>
        /// <param name="controller">A controller that has access to the <paramref name="viewName"/></param>
        /// <param name="viewName">The name of the view to render</param>
        /// <param name="viewModel">The model to use</param>
        /// <returns>The rendered view as a string</returns>
        public static string RenderView([NotNull]this ControllerBase controller, [AspMvcView][NotNull] string viewName, [AspMvcModelType][CanBeNull] object viewModel = null)
        {
            controller.ViewData.Model = viewModel;
            var controllerContext = controller.ControllerContext;
            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }

}