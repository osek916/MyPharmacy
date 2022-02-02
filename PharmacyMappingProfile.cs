using AutoMapper;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy
{
    public class PharmacyMappingProfile : Profile
    {
        public PharmacyMappingProfile()
        {
            CreateMap<Pharmacy, PharmacyDto>()
                .ForMember(p => p.City, pd => pd.MapFrom(g => g.Address.City))
                .ForMember(p => p.Street, pd => pd.MapFrom(g => g.Address.Street));


        }
    }
}
