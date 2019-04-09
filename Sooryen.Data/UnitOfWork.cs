
using Sooryen.Data.Interface;
using Sooryen.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Sooryen.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected TempSooryenDemoContext context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            context = new TempSooryenDemoContext();
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>();
        }

        /// <summary>
        /// Executes the sp.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<TEntity> ExecuteSp<TEntity>(string procedureName, params SqlParameter[] parameters) where TEntity : class
        {
            return ((IObjectContextAdapter)this.context).ObjectContext.ExecuteStoreQuery<TEntity>(PrepareCommandWithParametes(procedureName, parameters), parameters).ToList();
        }

        /// <summary>
        /// Executes the sp asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<List<TEntity>> ExecuteSpAsync<TEntity>(string procedureName, params SqlParameter[] parameters) where TEntity : class
        {
            var a = await ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQueryAsync<TEntity>(PrepareCommandWithParametes(procedureName, parameters), parameters);
            return a.ToList();
        }

        /// <summary>
        /// Prepares the command with parametes.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private string PrepareCommandWithParametes(string procedureName, params SqlParameter[] parameters)
        {
            StringBuilder cmdText = new StringBuilder("[" + procedureName + "] ");

            int count = 0;

            foreach (SqlParameter parameter in parameters)
            {
                cmdText.Append("@" + parameter.ParameterName + " = @" + parameter.ParameterName + "_param");

                if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                {
                    cmdText.Append(" out");
                }

                if (count < parameters.Length - 1)
                {
                    cmdText.Append(", ");
                }

                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }

                parameter.ParameterName = "@" + parameter.ParameterName + "_param";

                count++;
            }

            return cmdText.ToString();
        }
    }
}
