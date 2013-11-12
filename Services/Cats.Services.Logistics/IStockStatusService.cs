﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public interface IStockStatusService
    {
        IEnumerable<StockStatusViewModel> FreeStockStatusAsOF(DateTime date);
        IEnumerable<StockStatusViewModel> FreeStockByHub(int hubID);
        IEnumerable<Object> FreeStockByHubAsOF(DateTime date, int hubID);
    }
}