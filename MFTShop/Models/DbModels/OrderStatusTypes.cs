﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Models.DbModels
{
    public enum OrderStatusTypes
    {
        Open,
        Boxing,
        Sent,
        Canseled,
        NeedReview,
        TempEditOpen=1000,
        TempEditBoxing,
        TempEditSent,
        TempEditCanseled,
        TempEditNeedReview
    }
}
