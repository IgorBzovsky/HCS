using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API resource
            CreateMap<Location, KeyValuePairResource>();
            CreateMap<Utility, KeyValuePairResource>();
        }
    }
}
