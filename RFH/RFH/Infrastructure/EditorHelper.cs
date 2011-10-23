using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace RFH.Infrastructure {
    public static class EditorHelper {

        const string tag = @"
   <textarea name=""{0}"" id=""{0}""></textarea>
        <script type=""text/javascript"">

            CKEDITOR.replace('{0}'); 
            
        </script>";


        public static MvcHtmlString CKEditor(this HtmlHelper helper, string id) {
            return MvcHtmlString.Create(String.Format(tag, id));
        }

    }
}