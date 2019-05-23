using System;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class HtmlHelperExtension
    {
         public static string fo(this IHtmlHelper html, string key)
        {
            return key;
        }
        public static HtmlString foo(this IHtmlHelper html, string key)
        {
            return new HtmlString("<div style='font-size:18px;'>MyTestHtmlHelper</div>");
        }
         public static HtmlString ShowPageNavigate(this IHtmlHelper htmlHelper,MVC_DotNet.Models.PagerInfo Pager){
            return ShowPageNavigate(htmlHelper,Pager.CurrentPageIndex,Pager.PageSize,Pager.RecordCount);
         }
        public static HtmlString ShowPageNavigate(this IHtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var redirectTo = htmlHelper.ViewContext.HttpContext.Request.Path;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a> ", redirectTo, pageSize);
                if (currentPage > 1)
                {//处理上一页的连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a> ", redirectTo, currentPage - 1, pageSize);
                }

                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {//一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {//当前页处理              
                            output.AppendFormat("<a class='cpb' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a> ", redirectTo, currentPage, pageSize, currentPage);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a> ", redirectTo, currentPage + i - currint, pageSize, currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a> ", redirectTo, currentPage + 1, pageSize);
                }

                output.Append(" ");
                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a> ", redirectTo, totalPages, pageSize);
                }
                output.Append(" ");
            }
            output.AppendFormat("<label>第{0}页 / 共{1}页</label>", currentPage, totalPages);//这个统计加不加都行

            return new HtmlString(output.ToString());
        }
    }
    public static class HtmlHelperViewExtensions
    {
        public static HtmlString TestHtml(this IHtmlHelper htmlHelper)
        {
            return new HtmlString("<div style='font-size:18px;'>MyTestHtmlHelper</div>");
        }

        public static string TestHtml(this IHtmlHelper htmlHelper, string value)
        {
            return String.Format("<div>{0}</div>", value);
        }

        public static HtmlString JSHtml(this IHtmlHelper htmlHelper)
        {
            return new HtmlString("<script type=\"text/javascript\">alert('JSHtmlTest');</script>");
        }

    }
}