using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFH.Models {
   public class Slide {
      public string Src { get; set; }
      public string Alt { get; set; }
      public string Caption { get; set; }
      public string LinkText { get; set; }
      public string LinkUrl { get; set; }
   }
   public class ImageSliderModel {
      private readonly List<Slide> slides = new List<Slide>();
      public List<Slide> Slides { get { return slides; } }
   }
}