#pragma checksum "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c7eef358db114a3cfe6ade1a69fe1a642b43fd09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_Index), @"mvc.1.0.view", @"/Views/Employee/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\_ViewImports.cshtml"
using ClydesTakeoutPrototype;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\_ViewImports.cshtml"
using ClydesTakeoutPrototype.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
using ClydesTakeoutPrototype.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
using ClydesTakeoutPrototype.Models.OrderModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7eef358db114a3cfe6ade1a69fe1a642b43fd09", @"/Views/Employee/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f055c354c34352345c1b0adff39b7dada4e4c6a0", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Order>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Employee", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "OrderDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .menu-header {
        background: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url(/images/clydes.jpg);
        min-height: 100%;
        background-position: center;
        background-size: cover;
        color: white;
        text-align: center;
        vertical-align: middle;
        line-height: 90px;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li.item-list { 
        background: white; 
    }

    li:nth-child(odd).item-list { 
        background: WhiteSmoke; 
    }
</style>

<div class=""menu-header py-4"">
    <h1>Orders</h1>
</div>

<div class=""row pt-4"">
    <div class=""col-2"">
        <h4>Order Id</h4>
    </div>
    <div class=""col-3"">
        <h4>Customer</h4>
    </div>
    <div class=""col-3"">
        <h4>Pickup Time</h4>
    </div>
    <div class=""col-2"">
        <h4>Price</h4>
    </div>
    <div class=""col-2"">
        <h4>Select</h4>
    </div>
</div>
<hr />
<ul>
");
#nullable restore
#line 58 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
     foreach (var order in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"item-list\">\r\n            <div class=\"row py-2\">\r\n                <div class=\"col-2\">\r\n                    <p>");
#nullable restore
#line 63 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
                  Write(order.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"col-3\">\r\n                    <p>");
#nullable restore
#line 66 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
                  Write(order.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"col-3\">\r\n                    <p>");
#nullable restore
#line 69 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
                  Write(order.PickupTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"col-2\">\r\n                    <p>");
#nullable restore
#line 72 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
                  Write(order.Total.ToString("c2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"col-2\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c7eef358db114a3cfe6ade1a69fe1a642b43fd097493", async() => {
                WriteLiteral("View Order");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 75 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
                                                                                                             WriteLiteral(order.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </li>\r\n");
#nullable restore
#line 79 "C:\GitHub\ClydesTakeoutPrototype\ClydesTakeoutPrototype\Views\Employee\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
