using CrossModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CrossModel
{
    public class ColInfoHelper<T>
    {
        public List<ColInfo<T>> ColInfos = new List<ColInfo<T>>();

        public ColInfo<T> ColInfo { get; set; }

        public ColInfo<T> Add()
        {
            ColInfo<T> colInfo = new ColInfo<T>();

            ColInfos.Add(colInfo);

            return colInfo;
        }

        public ColInfo<T> Add(Expression<Func<T, object>> expression)
        {
            ColInfo<T> colInfo = new ColInfo<T>();

            if(expression.Body is MemberExpression)
            {
                colInfo._DataField = ((MemberExpression)expression.Body).Member.Name;
            }
            else
            {
                colInfo._DataField = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member.Name;
            }

            ColInfos.Add(colInfo);

            return colInfo;
        }
    }

    public class ColInfo<T>
    {
        private string gCaption = "";

        public string _Caption
        {
            get
            {
                return gCaption;
            }
            set
            {
                gCaption = value;
            }
        }

        public string _Format { get; set; }
        public float _Width { get; set; }
        public string _DataField { get; set; }
        public Alignment _Alignment { get; set; }
        public string _ExpressionText { get; set; }
        public string _ExpressionForeColor { get; set; }
        public bool _IsMerge { get; set; }
        public SpecialColumnType _SpecialColumnType { get; set; }

        public ColInfo<T> Caption(string c)
        {
            _Caption = c;
            return this;
        }

        public ColInfo<T> Format(string c)
        {
            _Format = c;
            return this;
        }

        public ColInfo<T> Width(float c)
        {
            _Width = c;
            return this;
        }

        public ColInfo<T> Align(Alignment c)
        {
            _Alignment = c;
            return this;
        }

        public ColInfo<T> ExpressionText(string c)
        {
            _ExpressionText = c;
            return this;
        }

        public ColInfo<T> ExpressionForeColor(string c)
        {
            _ExpressionForeColor = c;
            return this;
        }

        public ColInfo<T> ExpressionText(List<ColumnMapping> colMaps)
        {
            string result = MappingToExpress(colMaps, ExpressionType.Text);
            _ExpressionText = result;

            return this;
        }

        public ColInfo<T> ExpressionForeColor(List<ColumnMapping> colMaps)
        {
            string result = MappingToExpress(colMaps, ExpressionType.ForeColor);
            _ExpressionForeColor = result;

            return this;
        }

        public ColInfo<T> IsMerge(bool c)
        {
            _IsMerge = c;
            return this;
        }

        public ColInfo<T> SpecialColumnType(SpecialColumnType c)
        {
            _SpecialColumnType = c;
            return this;
        }

        /// <summary>
        /// 異動人員欄位
        /// </summary>
        public ColInfo<T> UserId()
        {
            return this.Caption("異動人員").Width(80).Align(Alignment.TopCenter);
        }

        /// <summary>
        /// 異動時間欄位
        /// </summary>
        public ColInfo<T> WTime()
        {
            return this.Caption("異動時間").Width(170).Format("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// 修改記號欄位
        /// </summary>
        public ColInfo<T> ModifyMark()
        {
            return this.Width(20).Align(Alignment.TopCenter);
        }

        private string MappingToExpress(List<ColumnMapping> colMaps, ExpressionType expressionType)
        {
            // 將List物件轉成Iif的報表判斷表達字串
            //範例：Iif([SLT_PRICE_FLUC] = 'P', '百分比', Iif([SLT_PRICE_FLUC] = 'F', '固定點數', ''))
            string field = "[" + _DataField + "]";
            string result = "";

            for (int i = 0; i < colMaps.Count; i++)
            {
                string whichValue = "Black";
                if (expressionType == ExpressionType.Text)
                {
                    whichValue = colMaps[i].DisplayText;
                }
                else if(expressionType == ExpressionType.ForeColor)
                {
                    if (!colMaps[i].ForeColor.IsEmpty)
                    {
                        whichValue = colMaps[i].ForeColor.Name;
                    }
                }

                result += "Iif(" + field + "='" + colMaps[i].Value + "','" + whichValue + "',";

                if (i == colMaps.Count - 1)
                    result += "''";
            }

            for (int i = 0; i < colMaps.Count; i++)
            {
                result += ")";
            }

            return result;
        }
    }

    public enum ExpressionType
    {
        Text,
        ForeColor
    }
}
