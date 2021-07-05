using System;
namespace XamarinPokedex.Models
{
    public class DoubleGridItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public bool FrameIsVisible { get; set; }

        public int Id2 { get; set; }
        public string Name2 { get; set; }
        public string Url2 { get; set; }
        public string Image2 { get; set; }
        public bool Frame2IsVisible { get; set; }
    }
}
