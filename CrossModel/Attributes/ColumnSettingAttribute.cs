using System;

namespace CrossModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)
]
    public class ColumnSettingAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public bool IsColumn { get; set; }

        public ColumnSettingAttribute(string displayName, bool isColumn = true)
        {
            DisplayName = displayName;
            IsColumn = isColumn;
        }
    }
}