using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Models.Entity;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EcommerceDemo.Utilities.Mapping;
using System.Net;

namespace EcommerceDemo.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseRepository _databaseRepository;
        public ProductRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task<IEnumerable<ProductModel>> GetProductById(int id)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[GetProductById]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ProductModel>();

                        while (await reader.ReadAsync())
                            list.Add(SqlMapper.Map<ProductModel>(reader));

                        return list;
                    }
                }
            }
        }

        public async Task<IEnumerable<ProductAttributeModel>> GetProductAttributesById(int id)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[GetProductAttributesById]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ProductAttributeModel>();

                        while (await reader.ReadAsync())
                            list.Add(SqlMapper.Map<ProductAttributeModel>(reader));

                        return list;
                    }
                }
            }
        }

        public async Task<IEnumerable<ProductListModel>> GetProducts(ProductSearchModel searchModel)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[GetAllProducts]";
                    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = searchModel.Name;
                    if (searchModel.pageList != null)
                    {
                        cmd.Parameters.Add("@RecordStart", SqlDbType.Int).Value = searchModel.pageList.RecordStart;
                        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = searchModel.pageList.PageSize;
                        cmd.Parameters.Add("@SortColumn", SqlDbType.VarChar).Value = searchModel.pageList.SortColumn;
                        cmd.Parameters.Add("@SortOrder", SqlDbType.VarChar).Value = searchModel.pageList.SortOrder;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ProductListModel>();

                        while (await reader.ReadAsync())
                            list.Add(SqlMapper.Map<ProductListModel>(reader));

                        return list;
                    }
                }
            }

        }

        public async Task<ResponseModel> SaveProduct(ProductModel model)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[SaveProduct]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = model.ProductId;
                    cmd.Parameters.Add("@ProdCatId", SqlDbType.Int).Value = model.ProdCatId;
                    cmd.Parameters.Add("@ProdName", SqlDbType.VarChar).Value = model.ProdName;
                    cmd.Parameters.Add("@ProdDescription", SqlDbType.VarChar).Value = model.ProdDescription;

                    long row = (long)await cmd.ExecuteScalarAsync();

                    if (row > 0)
                    {
                        foreach (var attribute in model.ProductAttributes)
                        {
                            using (var cmdAttributes = con.CreateCommand())
                            {
                                cmdAttributes.CommandText = "[dbo].[SaveProductAttribute]";
                                cmdAttributes.CommandType = CommandType.StoredProcedure;
                                cmdAttributes.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = row;
                                cmdAttributes.Parameters.Add("@AttributeId", SqlDbType.Int).Value = attribute.AttributeId;
                                cmdAttributes.Parameters.Add("@AttributeValue", SqlDbType.VarChar).Value = attribute.AttributeValue;

                                await cmdAttributes.ExecuteNonQueryAsync();
                            }
                        }
                        return new ResponseModel { Id = row.ToString(), StatusCode = HttpStatusCode.OK, Message = "Product has been saved successfully", Status = true };
                    }
                    else
                        return new ResponseModel { Id = row.ToString(), StatusCode = HttpStatusCode.BadRequest, Message = "Product has not been saved successfully", Status = false };
                }
            }
        }

        public async Task<ResponseModel> DeleteProduct(long productId)
        {
            using (var con = new SqlConnection(_databaseRepository.ConnectionString))
            {
                await con.OpenAsync();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "[dbo].[DeleteProduct]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductId", SqlDbType.BigInt).Value = productId;

                    var row = await cmd.ExecuteNonQueryAsync();

                    if (row > 0)
                        return new ResponseModel { Id = row.ToString(), StatusCode = HttpStatusCode.OK, Message = "Product has been deleted successfully", Status = true };
                    else
                        return new ResponseModel { Id = row.ToString(), StatusCode = HttpStatusCode.BadRequest, Message = "Product has not been deleted successfully", Status = false };
                }
            }
        }
    }
}
