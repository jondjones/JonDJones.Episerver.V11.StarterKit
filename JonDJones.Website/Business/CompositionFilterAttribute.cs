using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JonDJones.Website.Business
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CompositionFilterAttribute : ActionFilterAttribute
    {
        private HtmlTextWriter _tw;
        private StringWriter _sw;
        private StringBuilder _sb;
        private HttpWriter _output;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sb = new StringBuilder();
            _sw = new StringWriter(_sb);
            _tw = new HtmlTextWriter(_sw);
            _output = (HttpWriter)filterContext.RequestContext.HttpContext.Response.Output;
            filterContext.RequestContext.HttpContext.Response.Output = _tw;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                var contentRetriver = new ContentRetriever();
                var edgeSideInclude = new EdgeSideIncludeParser();

                var compositionService = new CompositionService(contentRetriver, edgeSideInclude);

                var output = filterContext.RequestContext.HttpContext.Request.Filter.ToString();
                var processedHtml = compositionService.ProcessEdgeSideIncludes(output);
                _output.Write(processedHtml);
            }
        }
    }
}