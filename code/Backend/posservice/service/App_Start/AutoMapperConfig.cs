using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using posrepository.DTO;
using mrgvn.db;

namespace service
{
    public enum Action : int
    {
        CREATE = 1,
        READALL = 2,
        READID = 3,
        UPDATE = 4,
        DELETE = 5,
    }
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<ProductDTO, PRODUCT>().
                ForMember(y => y.id, x => x.MapFrom(src => src.idproducts)).
                ReverseMap();

                //config.CreateMap<EntryDTO, PRODUCTENTRy>().
                ////ForMember(dst => dst.PRODUCTENTRYDETAILS, src => src.MapFrom(x => x.details.idproducts)).
                //ForMember(dst => dst.PRODUCTENTRYDETAILS.Select( t=> t.quantity), src => src.MapFrom(x => x.details.quantity)).
                //ReverseMap();

                config.CreateMap<SALE, SalesDTO>().
                ForMember(x => x.Saledetails, src => src.MapFrom(x => x.SALEDETAILS)).
                ForMember(x1 => x1.idsales, src1 => src1.MapFrom(x1 => x1.id)).
                ReverseMap();

                config.ValidateInlineMaps = false;
            });

            // var config = new MapperConfiguration(cfg => cfg.CreateMap<PRODUCT, ProductDTO>());

        }
    }
}