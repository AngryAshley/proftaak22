#pragma checksum "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3037acfc2633ae5f3ce330a8185cab2f2532edc6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\_ViewImports.cshtml"
using RailViewClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\_ViewImports.cshtml"
using RailViewClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3037acfc2633ae5f3ce330a8185cab2f2532edc6", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbd42e8d34f7f43503d6974f4093dc0266f3edfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Notification>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n    <div class=\"row d-flex justify-content-md-around\" id=\"rowtest\">\r\n        <div class=\"col-4 div-containers\" id=\"tester\">\r\n            <h4>Meldingen</h4>\r\n            <div id=\"log\" class=\"border border-dark border-2 div-containers\">\r\n");
#nullable restore
#line 19 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"text-center border-top border-dark border-2\">\r\n                        <p class=\"px-2 pt-2 text-capitalize\">\r\n                            ");
#nullable restore
#line 23 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
                       Write(item.Times);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />");
#nullable restore
#line 23 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
                                        Write(item.Route);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br /><b>");
#nullable restore
#line 23 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
                                                            Write(item.Alert);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n                        </p>\r\n                    </div>\r\n");
#nullable restore
#line 26 "D:\Documents\SchoolFontys\proftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>

            <input type=""button"" class=""btn btn-success my-2"" id=""btnpopup"" value=""Bel 112"" onclick=""ShowPopUp();"" />
            <input type=""button"" class=""btn btn-success my-2"" id=""liveToastBtn"" value=""Test"" onclick=""ShowToast();""/>
            <input type=""button"" class=""btn btn-success my-2"" id=""btntrain"" value=""Hide Active Trains"" onclick=""ShowAndHideTrains();"" />

        </div>
        <div class=""col-8 map-style"">
            <div id=""map"" class=""map-style""></div>
        </div>
    </div>
    <div class=""row d-flex"">
        <div id=""toastplaceholder"" class=""d-flex"">
            <div class=""toast float-right w-100"" role=""alert"" aria-live=""assertive"" aria-atomic=""true"" data-autohide=""false"">
                <div class=""toast-header"">
                    <strong class=""mr-auto"">PERSON DETECTED</strong>
                    <small>11 mins ago</small>
                    <button type=""button"" class=""ml-2 mb-1 close"" data-dismiss=""toast"" aria-label=""Close"">
          ");
            WriteLiteral("              <span aria-hidden=\"true\">&times;</span>\r\n                    </button>\r\n                </div>\r\n                <div class=\"toast-body\">CAMERA 01: Ehv - Hmbv</div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            WriteLiteral("\r\n");
            DefineSection("Css", async() => {
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet@1.3.4/dist/leaflet.css\" />\r\n    <link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.css\" />\r\n");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script src=\"https://unpkg.com/leaflet@1.3.4/dist/leaflet.js\"></script>\r\n    <script src=\"https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.js\"></script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Notification>> Html { get; private set; }
    }
}
#pragma warning restore 1591
