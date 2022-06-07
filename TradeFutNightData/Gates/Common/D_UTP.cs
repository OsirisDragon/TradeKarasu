﻿using DataEngine;
using LinqToDB;
using System.Linq;
using TradeFutNightData.Models.Common;

namespace TradeFutNightData.Gates.Common
{
    public class D_UTP : D_UTP<UTP>
    {
        public D_UTP(DalSession das)
        {
            this._das = das;
        }
    }

    public class D_UTP<T> : ParentGate
    {
        public void Delete(string TXN_ID)
        {
            _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_TXN_ID == TXN_ID)
            .Delete();
        }

        public void DeleteUser(string USER_ID)
        {
            _das.DataConn.GetTable<UTP>()
                .Where(c => c.UTP_USER_ID == USER_ID)
            .Delete();
        }
    }
}