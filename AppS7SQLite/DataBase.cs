using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppS7SQLite
{
   public interface DataBase
    {
        SQLiteAsyncConnection GetConnection();
    }
}
