using Dapper;
using LinqToDB;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace DataEngine
{
    public class ParentGate
    {
        protected DalSession _das = null;

        public ParentGate()
        {
        }

        public static CommandDefinition BuildCommand<T>(string commandText, object inputParams = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?), CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (inputParams != null)
            {
                // 如果是單一SQL語句的參數
                if (inputParams is DynamicParameters)
                {
                }
                // 如果是多個SQL語句的參數，通常是將畫面上的整個資料集IEnumerable<T>傳進來
                else
                {
                    var parameters = new List<DynamicParameters>();

                    var data = (IEnumerable<T>)inputParams;

                    // 取得SQL語句中的參數
                    // 用RegularExpression抓出開頭為@的字串
                    // [a-zA-Z_0-9()]是找出大小寫英文字母、底線、左右括號的字元
                    // 最後面的+號是多個字元的意思
                    MatchCollection matchList = Regex.Matches(commandText, @"@[a-zA-Z_0-9]+");
                    var regexParams = matchList.Cast<Match>().Select(match => match.Value).ToList();

                    // 將參數轉成Dapper用的參數
                    foreach (var item in data)
                    {
                        var p = new DynamicParameters();

                        foreach (var prop in item.GetType().GetProperties())
                        {
                            string propName = prop.Name;
                            object objValue = prop.GetValue(item);

                            // 如果有在SQL語句裡面的參數才要加進來
                            if (regexParams.Contains("@" + prop.Name))
                            {
                                p.Add("@" + propName, objValue);
                            }

                            // 如果有要未修改前的資料的參數的話
                            if (propName == "OriginalData" && objValue != null)
                            {
                                foreach (var propInner in objValue.GetType().GetProperties())
                                {
                                    if (regexParams.Contains("@OriginalData_" + propInner.Name))
                                    {
                                        p.Add("@OriginalData_" + propInner.Name, propInner.GetValue(objValue));
                                    }
                                }
                            }
                        }

                        parameters.Add(p);
                    }

                    inputParams = parameters;
                }
            }

            CommandDefinition resultCommand = new CommandDefinition(
                commandText: commandText,
                commandType: commandType,
                parameters: inputParams,
                transaction: transaction
            );

            return resultCommand;
        }

        public virtual void Insert<T>(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.Insert(item);
            }
        }

        public virtual void Delete<T>(IEnumerable<T> data)
        {
            foreach (var item in data)
            {
                _das.DataConn.Delete(item);
            }
        }
    }
}