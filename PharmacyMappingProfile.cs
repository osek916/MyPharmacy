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
            //mapuje z --> do
            CreateMap<Pharmacy, PharmacyDto>()
                .ForMember(p => p.City, pd => pd.MapFrom(g => g.Address.City))
                .ForMember(p => p.Street, pd => pd.MapFrom(g => g.Address.Street))
                .ForMember(p => p.PostalCode, pd => pd.MapFrom(g => g.Address.PostalCode));

           

            CreateMap<PharmacyDto, Pharmacy>();

            CreateMap<CreatePharmacyDto, Pharmacy>()
                .ForMember(p => p.Address, pd => pd.MapFrom(dto => new Address() { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<UpdatePharmacyDto, Pharmacy>()
                .ForMember(p => p.Address, pd => pd.MapFrom(dto => new Address() { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<DrugDto, Drug>();
            CreateMap<Drug, DrugDto>();
            CreateMap<CreateDrugDto, Drug>();
            CreateMap<Drug, UpdateDrugDto>();

        }
    }
}
