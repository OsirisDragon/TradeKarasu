using CrossModel.Attributes;
using LinqToDB.Mapping;

namespace CrossModel
{
    public class DtoParent<T>
    {
        [NotColumn]
        [ColumnSetting(displayName: "OriginalData", isColumn: false)]
        public virtual T OriginalData { get; set; }

        [NotColumn]
        [ColumnSetting(displayName: "ModifyMark", isColumn: false)]
        public virtual string ModifyMark { get; set; }
    }
}