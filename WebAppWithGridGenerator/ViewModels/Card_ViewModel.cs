using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppWithGridGenerator.ViewModels
{
    public class Card_ViewModel
    {
        public string ImgUrl { get; set; }
        public string ImgAlt { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public List<string> DescriptionParagraphs { get; set; }
        public string DetailsUrl { get; set; }
    }
}
