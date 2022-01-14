#pragma checksum "D:\School\Fontys\TalkToMeProftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "62d3c4f581caa60d2e74196c0f462a098c411a15"
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
#line 1 "D:\School\Fontys\TalkToMeProftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\_ViewImports.cshtml"
using RailViewClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\School\Fontys\TalkToMeProftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\_ViewImports.cshtml"
using RailViewClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"62d3c4f581caa60d2e74196c0f462a098c411a15", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbd42e8d34f7f43503d6974f4093dc0266f3edfd", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\School\Fontys\TalkToMeProftaak22\Software\RailViewClient_ASP\RailViewClient\RailViewClient\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row d-flex justify-content-md-around\">\r\n    <div class=\"col-3 notifications\">\r\n        <h4>Meldingen</h4>\r\n        <div id=\"log\" class=\"border border-dark border-2 notifications\">\r\n");
            WriteLiteral(@"        </div>

        <input type=""button"" class=""btn btn-success my-2"" id=""btnpopup"" value=""Bel 112"" onclick=""ShowPopUp();"" />
        <input type=""button"" class=""btn btn-success my-2"" id=""liveToastBtn"" value=""Test"" onclick=""ShowToast();"" />
        <input type=""button"" class=""btn btn-success my-2"" id=""btntrain"" value=""Hide Active Trains"" onclick=""ShowAndHideTrains();"" />
    </div>
    <div class=""col-9 map-style"">
        <div id=""map"" class=""map-style""></div>
    </div>
</div>
<div class=""row"">
    <div id=""notification-area"" ");
            WriteLiteral(">\r\n    </div>\r\n    <!--<div id=\"toastplaceholder\" class=\"w-100 vh-100 fixed-bottom\">-->\r\n");
            WriteLiteral(@"    <!--</div>-->
</div>

<template id=""all-data-template"">
    {{#.}}
    <div class=""text-center border-top border-dark border-2"">
        <p class=""px-2 pt-2 text-capitalize"">
            {{Times}} <br /> {{Route}} <br /> <b>{{Alert}}</b>
        </p>
    </div>
    {{/.}}
</template>

");
            DefineSection("Css", async() => {
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet@1.3.4/dist/leaflet.css\" />\r\n    <link rel=\"stylesheet\" href=\"https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.css\" />\r\n");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://unpkg.com/leaflet@1.3.4/dist/leaflet.js""></script>
    <script src=""https://unpkg.com/leaflet-routing-machine@latest/dist/leaflet-routing-machine.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/mustache.js/2.3.0/mustache.js""></script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
