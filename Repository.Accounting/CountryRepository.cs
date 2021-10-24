using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IRepository.Accounting;
using Microsoft.Extensions.Configuration;
using ViewModel.Accounting.ViewModels;

namespace Repository.Accounting
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IConfiguration _config;

        public CountryRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection => new SqlConnection(_config.GetConnectionString("AccountingConnectionString"));

        public async Task<List<CountryViewModel>> Get()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = @"SELECT [CountryId]
                    ,[CountryCode]
                    ,[CountryName]
                    ,[DefaultCode]
                    ,[Population]
                    ,[Area]
                    ,[Distance]
                    ,[LenWid]
                    ,[Capital]
                    ,[Time] FROM [BASE].[Table_000_CountryInfo]";
                conn.Open();
                var result = await conn.QueryAsync<CountryViewModel>(sQuery,null);
                return result.ToList();
            }
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(CountryViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Add(CountryViewModel model)
        {
            int rowAffected = 0;
            using (IDbConnection conn = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CountryCode", model.CountryCode);
                parameters.Add("@CountryName", model.CountryName);
                parameters.Add("@DefaultCode", model.DefaultCode);
                parameters.Add("@Population", model.Population);
                parameters.Add("@Area", model.Area);
                parameters.Add("@Distance", model.Distance);
                parameters.Add("@LenWid", model.LenWid);
                parameters.Add("@Capital", model.Capital);
                parameters.Add("@Time", model.Time);

                rowAffected = await conn.ExecuteAsync(@"INSERT INTO [BASE].[Table_000_CountryInfo] ([CountryCode], [CountryName], [DefaultCode], [Population], [Area], [Distance], [LenWid], [Capital], [Time])
                                                 VALUES (@CountryCode, @CountryName, @DefaultCode, @Population, @Area, @Distance, @LenWid, @Capital, @Time);", parameters, commandType: CommandType.Text);
            }

            return rowAffected;
        }
    }
}
