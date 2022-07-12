using Dapper;
using DataEngine;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using TradeOptNightData.Models.Common;

namespace TradeOptNightData.Gates.Common
{
    public class D_PGRP : D_PGRP<PGRP>
    {
        public D_PGRP(DalSession das){ this._das = das; }
    }

    public class D_PGRP<T> : ParentGate
    {
        public int UpdateDspConfirm(int PGRP_OSW_GRP, int PGRP_DSP_GRP)
        {
            int affectedRows = -1;

            affectedRows = _das.DataConn.GetTable<PGRP>()
                .Where(c => c.PGRP_OSW_GRP == PGRP_OSW_GRP && c.PGRP_DSP_GRP == PGRP_DSP_GRP)
                .Set(c => c.PGRP_DSP_CONFIRM, 'Y')
                .Update();

            return affectedRows;
        }
    }
}
