using AutoMapper;
using Hit.Services.DTOs;
using Hit.Services.DTOs.HitServices;
using Hit.Services.DTOs.Protel;
using Hit.Services.Models.Models.ProtelMappingsToPylon;
using Hit.Services.Models.Models.SQL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Hit.Services.Models.AutoMapperConfig), "RegisterMappings")]
namespace Hit.Services.Models
{

    public class AutoMapperConfig
    {

        /// <summary>
        /// Register Mappings between Models
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SqlParameters, SqlParametersDTO>();
                cfg.CreateMap<SqlParametersDTO, SqlParameters>();

                // Pylon Bridge Mappings
                cfg.CreateMap<FiscalcdModel, FiscalcdDTO>();
                cfg.CreateMap<FiscalcdDTO, FiscalcdModel>();

                cfg.CreateMap<LizenzModel, LizenzDTO>();
                cfg.CreateMap<LizenzDTO, LizenzModel>();

                cfg.CreateMap<MwstModel, MwstDTO>();
                cfg.CreateMap<MwstDTO, MwstModel>();

                cfg.CreateMap<UktoModel, UktoDTO>();
                cfg.CreateMap<UktoDTO, UktoModel>();

                cfg.CreateMap<ZahlartModel, ZahlartDTO>();
                cfg.CreateMap<ZahlartDTO, ZahlartModel>();
            });
        }
    }
}
