using System.Collections.Generic;

namespace CrossModel
{
    public class MenuEntity
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public bool Expanded { get; set; }
        public string AreaName { get; set; }
        public string PrefixName { get; set; }
        public List<MenuEntity> Items { get; set; }
    }
}
