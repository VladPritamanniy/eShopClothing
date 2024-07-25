using Microsoft.AspNetCore.Mvc.Rendering;

namespace  Web.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Email => "Email";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string[] Products => new[] { "Products", "CreateProduct" };

        public static string? IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string? EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string? ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string? ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string? ProductsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Products);

        public static string? PageNavClass(ViewContext viewContext, params string[] page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            for(int i = 0; i < page.Length; i++)
            {
                if(string.Equals(activePage, page[i], StringComparison.OrdinalIgnoreCase))
                {
                    return "active";
                }
            }
            return null;
        }
    }
}
