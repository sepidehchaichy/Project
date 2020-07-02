
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using WebApplication1.Data;

namespace Data
{

    public class EntitiesContext : IdentityDbContext<IdentityUser>
    {
        public EntitiesContext()
         : base("MagfaConnection")
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = 180;
        }

        public DbSet<Magfa> Magfa { get; set; }

        public DataTable ExecuteStoredProcedure(EntitiesContext DataContext, string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            using (var DX = new EntitiesContext())
            {
                var conn = DX.Database.Connection;
                var initialState = conn.State;
                var dt = new DataTable();
                try
                {
                    if (initialState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = storedProcedureName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.Add(parameter);
                            }
                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                reader.Close();
                            }
                        }
                        else
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                reader.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (initialState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

    }
}
