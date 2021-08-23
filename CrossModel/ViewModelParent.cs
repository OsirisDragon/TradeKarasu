using CrossModel.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CrossModel
{
    public class ViewModelParent<T>
    {
        private string _MemoText;

        [JsonConverter(typeof(StringEnumConverter))]
        public RunStatus RunStatus { get; set; }

        public ButtonStatus ButtonStatus = new ButtonStatus();
        public IEnumerable<string> TableKeys { get; set; }
        public string MemoText { get => _MemoText; set => _MemoText = value; }
        public string ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramIDName { get; set; }
        public IEnumerable<DataGridColumn> DataGridColumns { get; set; }
        public IEnumerable<T> MainData { get; set; }
        public string FailError { get; set; }
    }

    public enum RunStatus
    {
        [EnumMember(Value = "Loading")]
        Loading = 0,

        [EnumMember(Value = "NotAllowed")]
        NotAllowed = 1,

        [EnumMember(Value = "CanRun")]
        CanRun = 2,

        [EnumMember(Value = "Completed")]
        Completed = 3,

        [EnumMember(Value = "FailError")]
        FailError = 4
    }

    public class ButtonStatus
    {
        public bool EnableInsert = false;
        public bool EnableSave = false;
        public bool EnableRemove = false;
        public bool EnablePrint = true;
    }

    public class DataGridColumn
    {
        public string DataField { get; set; }
        public string Caption { get; set; }
        public string Format { get; set; }
        public int Width { get; set; }
        public DataGridColumnAlignment Alignment { get; set; }
        public Lookup Lookup { get; set; }
        public CustomAttribute CustomAttribute { get; set; }
    }

    public class Lookup
    {
        public IEnumerable<LookupProperty> Datasource { get; set; }
    }

    public class LookupProperty
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ForeColor { get; set; }
    }

    public class CustomAttribute
    {
        private SpecialColumnType _specialColumnType = SpecialColumnType.None;
        private bool _isMerge = false;

        public SpecialColumnType SpecialColumnType { get => _specialColumnType; set => _specialColumnType = value; }

        public bool IsMerge { get => _isMerge; set => _isMerge = value; }
    }

    public enum DataGridColumnAlignment
    {
        None,
        Center,
        Left,
        Right
    }
}