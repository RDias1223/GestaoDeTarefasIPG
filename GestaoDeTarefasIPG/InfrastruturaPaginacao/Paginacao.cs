using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using GestaoDeTarefasIPG.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoDeTarefasIPG.InfrastruturaPaginacao
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("div",Attributes="modelo-pagina")]
    public class Paginacao : TagHelper
    {
      
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           
        }  
    }
}
