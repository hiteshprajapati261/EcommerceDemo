using AutoMapper;
using EcommerceDemo.Models.Entity;
using EcommerceDemo.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemo.Models.MappingConfiguration
{
    public static class MappingExtensions
    {

        public static ProductModel ToModel(this ProductEntity entity)
        {
            return Mapper.Map<ProductEntity, ProductModel>(entity);
        }
        public static ProductEntity ToEntity(this ProductModel model)
        {
            return Mapper.Map<ProductModel, ProductEntity>(model);
        }
    }
}
