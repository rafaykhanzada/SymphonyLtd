using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SymphonyLtd.Security
{
    public class ImageValidation
    {
        public static bool IsImage(HttpPostedFileBase imgup)
        {
            string[] formats = new string[] { ".jpg", ".png", ".jpeg", ".bmp" }; //Extentions

            if (imgup.ContentType.Contains("image") && formats.Any(item => imgup.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }
    }
}