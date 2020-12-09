using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EcommerceDemo.Utilities.Mapping;

namespace EcommerceDemo.Data.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ProductCategoryRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task<IEnumerable<ComboBoxModel>> GetCategories()
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[GetAllCategories]";                   
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ComboBoxModel>();

                        while (await reader.ReadAsync())
                            list.Add(SqlMapper.Map<ComboBoxModel>(reader));

                        return list;
                    }
                }
            }
        }

    }
}
