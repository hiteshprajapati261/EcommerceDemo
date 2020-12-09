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
    public static class AutoMapperStartupTask
    {
        public static void Execute()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductEntity>();
                cfg.CreateMap<ProductEntity, ProductModel>();
            });
        }
    }
}
