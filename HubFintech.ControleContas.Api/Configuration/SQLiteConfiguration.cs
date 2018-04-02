﻿using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace HubFintech.ControleContas.Api.Configuration
{
    public abstract class SqLiteConfiguration : DbConfiguration
    {
        protected SqLiteConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite",
                (DbProviderServices) SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
        }
    }
}