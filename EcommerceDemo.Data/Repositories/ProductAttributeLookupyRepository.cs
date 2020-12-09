using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EcommerceDemo.Utilities.Mapping;

namespace EcommerceDemo.Data.Repositories
{
    public class ProductAttributeLookupRepository : IProductAttributeLookupRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ProductAttributeLookupRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task<IEnumerable<ProductAttributeLookupModel>> GetProductAttributeLookups(int categoryId)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[GetAllProductAttributeLookups]";
                    cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ProductAttributeLookupModel>();

                        while (await reader.ReadAsync())
                            list.Add(SqlMapper.Map<ProductAttributeLookupModel>(reader));

                        return list;
                    }
                }
            }
        }
       
    }
}
