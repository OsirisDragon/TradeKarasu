using LinqToDB.Mapping;

namespace CrossModel
{
    public class DtoParent<T>
    {
        [NotColumn]
        public virtual T OriginalData { get; set; }

        [NotColumn]
        public virtual string ModifyMark { get; set; }
    }
}