namespace CrossModel.Enum
{
    /// <summary>
    /// 0:AD帳號+密碼+卡
    /// 1:AD帳號+密碼
    /// 2:AD帳號+卡
    /// 3:AD帳號
    /// </summary>
    public enum MparAuthType
    {
        AdPassCard = 0,
        AdPass = 1,
        AdCard = 2,
        Ad = 3
    }
}