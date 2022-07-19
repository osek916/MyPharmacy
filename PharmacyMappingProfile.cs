using AutoMapper;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using MyPharmacy.Models.OrderByClientDtos;
using MyPharmacy.Models.OrderForPharmacyDtos;

namespace MyPharmacy
{
    public class PharmacyMappingProfile : Profile
    {
        public PharmacyMappingProfile()
        {
            //mapping from --> to
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
            CreateMap<Drug, DrugDto>()
                .ForMember(d => d.DrugsName, dd => dd.MapFrom(g => g.DrugInformation.DrugsName))
                .ForMember(d => d.SubstancesName, dd => dd.MapFrom(g => g.DrugInformation.SubstancesName))
                .ForMember(d => d.NumberOfTablets, dd => dd.MapFrom(g => g.DrugInformation.NumberOfTablets))
                .ForMember(d => d.MilligramsPerTablets, dd => dd.MapFrom(g => g.DrugInformation.MilligramsPerTablets))
                .ForMember(d => d.LumpSumDrug, dd => dd.MapFrom(g => g.DrugInformation.LumpSumDrug))
                .ForMember(d => d.PrescriptionRequired, dd => dd.MapFrom(g => g.DrugInformation.PrescriptionRequired));


            CreateMap<Drug, UpdateDrugDto>();


            CreateMap<Pharmacy, SearchEnginePharmacyDto>()
                .ForMember(p => p.City, pd => pd.MapFrom(g => g.Address.City))
                .ForMember(p => p.Street, pd => pd.MapFrom(g => g.Address.Street))
                .ForMember(p => p.PostalCode, pd => pd.MapFrom(g => g.Address.PostalCode));

            CreateMap<DrugInformation, SearchEngineDrugInformationDto>()
                .ForMember(d => d.Description, dd => dd.MapFrom(g => g.Description));

            CreateMap<Pharmacy, SearchEngineDrugDto>();
            CreateMap<Drug, SearchEngineDrugDto>()
                .ForMember(d => d.City, dd => dd.MapFrom(g => g.Pharmacy.Address.City))
                .ForMember(d => d.Street, dd => dd.MapFrom(g => g.Pharmacy.Address.Street))
                .ForMember(d => d.PostalCode, dd => dd.MapFrom(g => g.Pharmacy.Address.PostalCode))
                .ForMember(d => d.SubstancesName, dd => dd.MapFrom(g => g.DrugInformation.SubstancesName))
                .ForMember(d => d.DrugsName, dd => dd.MapFrom(g => g.DrugInformation.DrugsName))
                .ForMember(d => d.LumpSumDrug, dd => dd.MapFrom(g => g.DrugInformation.LumpSumDrug))
                .ForMember(d => d.PrescriptionRequired, dd => dd.MapFrom(g => g.DrugInformation.PrescriptionRequired))
                .ForMember(d => d.MilligramsPerTablets, dd => dd.MapFrom(g => g.DrugInformation.MilligramsPerTablets))
                .ForMember(d => d.NumberOfTablets, dd => dd.MapFrom(g => g.DrugInformation.NumberOfTablets));

            CreateMap<DrugInformation, DrugInformationDto>();

            CreateMap<CreateDrugInformationDto, DrugInformation>();

            CreateMap<DrugCategory, DrugCategoryDto>();

            CreateMap<CreateDrugCategoryDto, DrugCategory>();

            CreateMap<UpdateDrugCategoryDto, DrugCategory>();

            CreateMap<User, UserDto>();

            CreateMap<OrderForPharmacy, OrderForPharmacyDto>();

            CreateMap<CreateOrderForPharmacyDto, OrderForPharmacy>();

            CreateMap<CreateOrderForPharmacyDrugDto, Drug>();

            CreateMap<OrderByClient, OrderByClientDto>();
        }
    }
}
