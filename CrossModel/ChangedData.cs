using System.Collections.Generic;

namespace CrossModel
{
    public class ChangedData<T>
    {
        public IEnumerable<T> AddedItems { get; set; }
        public IEnumerable<T> ChangedItems { get; set; }
        public IEnumerable<T> DeletedItems { get; set; }
    }
}