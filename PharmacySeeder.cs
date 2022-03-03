using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyPharmacy
{
    public class PharmacySeeder
    {
        private readonly PharmacyDbContext _dbContext;
        public PharmacySeeder(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                if(_dbContext.Database.IsRelational())
                {
                    var migrations = _dbContext
                    .Database
                    .GetPendingMigrations();
                    if (migrations.Any() && migrations != null)
                        _dbContext.Database.Migrate();
                }
                


                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Statuses.Any())
                {
                    var statuses = GetStatuses();
                    _dbContext.Statuses.AddRange(statuses);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.DrugCategories.Any())
                {
                    var drugCategories = GetDrugCategories();
                    _dbContext.DrugCategories.AddRange(drugCategories);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.DrugInformations.Any())
                {
                    var drugInformations = GetDrugInformations();
                    _dbContext.DrugInformations.AddRange(drugInformations);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Pharmacies.Any())
                {
                    var pharmacies = GetPharmacy();
                    _dbContext.Pharmacies.AddRange(pharmacies);
                    _dbContext.SaveChanges();
                }
            }
        }

        

        private IEnumerable<Status> GetStatuses()
        {
            var statuses = new List<Status>()
            {
                new Status()
                {
                    Name = "delivered"
                },
                new Status()
                {
                    Name = "during"
                },
                new Status()
                {
                    Name = "failed"
                }
            };
            return statuses;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            { 
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Pharmacist"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };

            return roles;
        }
        
        public IEnumerable<Pharmacy> GetPharmacy()
        {
            var drugsInformations = _dbContext.DrugInformations;
            var pharmacies = new List<Pharmacy>()
            {
                new Pharmacy()
                {
                    Name = "Pod Slowikiem",
                    ContactEmail = "poslowikiem@wp.pl",
                    ContactNumber = "777777777",
                    Address = new Address()
                    {
                        City = "Szczecin",
                        Street = "Wolna 4",
                        PostalCode = "50-101"
                    },
                    HasPresciptionDrugs = true,
                    Drugs = new List<Drug>()
                    {
                        new Drug()
                        {
                            AmountOfPackages = 5,
                            Price = 16,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Relanium" && d.NumberOfTablets == 30)
                        },
                        new Drug()
                        {

                            AmountOfPackages = 4,
                            Price = 15,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Morfeo" && d.NumberOfTablets == 20)
                        },
                        new Drug()
                        {
                            
                            AmountOfPackages = 11,
                            Price = 20,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Noopeptil" && d.NumberOfTablets == 10)
                        },
                        new Drug()
                        {

                            AmountOfPackages = 12,
                            Price = 22,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Medikinet" && d.NumberOfTablets == 30)
                        },
                        new Drug()
                        {

                            AmountOfPackages = 13,
                            Price = 23,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Concerta" && d.NumberOfTablets == 30)
                        }
                    }
                },
                new Pharmacy()
                {
                    Name = "Puls",
                    ContactEmail = "puls@wp.pl",
                    ContactNumber = "123456789",
                    Address = new Address()
                    {
                        City = "Wrocław",
                        Street = "Szybka 2",
                        PostalCode = "23-545"
                    },
                    HasPresciptionDrugs = true,
                    Drugs = new List<Drug>()
                    {
                        new Drug()
                        {
                            AmountOfPackages = 7,
                            Price = 10,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Apap" && d.NumberOfTablets == 30)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 8,
                            Price = 21,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Apap" && d.NumberOfTablets == 20)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 18,
                            Price = 20,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Imovane" && d.NumberOfTablets == 20)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 24,
                            Price = 20,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Relanium" && d.NumberOfTablets == 30)
                        }
                    }
                },

            new Pharmacy()
            {
                Name = "Pod Slowikiem",
                ContactEmail = "poslowikiem@wp.pl",
                ContactNumber = "777777777",
                Address = new Address()
                {
                    City = "Szczecin",
                    Street = "Wolna 4",
                    PostalCode = "50-101"
                },
                HasPresciptionDrugs = true,
                Drugs = new List<Drug>()
                    {
                        new Drug()
                        {
                            AmountOfPackages = 3,
                            Price = 13,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Relanium" && d.NumberOfTablets == 30)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 4,
                            Price = 15,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Stilnox" && d.NumberOfTablets == 20)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 7,
                            Price = 20,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Noopeptil" && d.NumberOfTablets == 10)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 8,
                            Price = 23,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Gripex" && d.NumberOfTablets == 10)
                        },
                        new Drug()
                        {
                            AmountOfPackages = 8,
                            Price = 3,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Dobroson" && d.NumberOfTablets == 20)

                        },
                        new Drug()
                        {
                            AmountOfPackages = 3,
                            Price = 33,
                            DrugInformation = drugsInformations.FirstOrDefault(d => d.DrugsName == "Medikinet" && d.NumberOfTablets == 30)

                        }
                    }
            }
            };

            return pharmacies;
        }

        public IEnumerable<DrugInformation> GetDrugInformations()
        {
            var drugCategoriesList = _dbContext.DrugCategories;
            var drugInformations = new List<DrugInformation>()
            {
                new DrugInformation()
                {
                            DrugsName = "Relanium",
                            SubstancesName = "Diazepam",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 5,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Antipsychotic" || d.CategoryName == "Anxiolytic").ToList()

                },
                new DrugInformation()
                {
                            DrugsName = "Morfeo",
                            SubstancesName = "Zaleplon",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 10,
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Hypnotic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Noopeptil",
                            SubstancesName = "Noopept",
                            NumberOfTablets = 10,
                            MilligramsPerTablets = 10,
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Nootropic" ).ToList()

                },
                new DrugInformation()
                {
                            DrugsName = "Medikinet",
                            SubstancesName = "Metylofenidat",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 20,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Stimulant" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Concerta",
                            SubstancesName = "Metylofenidat",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 18,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Stimulant" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Apap",
                            SubstancesName = "Paracetamol",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 500,
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Painkiller" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Apap",
                            SubstancesName = "Paracetamol",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 500,
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Painkiller" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Imovane",
                            SubstancesName = "Zopiklon",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 7,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Hypnotic" ).ToList()
               },
                new DrugInformation()
                {
                            DrugsName = "Nootropil",
                            SubstancesName = "Piracetam",
                            NumberOfTablets = 60,
                            MilligramsPerTablets = 1200,
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Nootropic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Stilnox",
                            SubstancesName = "Zolpidem",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 10,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Hypnotic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Xanax",
                            SubstancesName = "Alprazolam",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 1,
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Antipsychotic" || d.CategoryName == "Anxiolytic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Dobroson",
                            SubstancesName = "Zopiklon",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 7,
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Hypnotic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Gripex",
                            SubstancesName = "Dekstrometorfan",
                            NumberOfTablets = 10,
                            MilligramsPerTablets = 15,
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Painkiller" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Zomiren",
                            SubstancesName = "Alprazolam",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 1,
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Antipsychotic" || d.CategoryName == "Anxiolytic" ).ToList()
                },
                new DrugInformation()
                {
                            DrugsName = "Ibuprom",
                            SubstancesName = "Ibuprofen",
                            NumberOfTablets = 10,
                            MilligramsPerTablets = 300,
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            DrugCategories = drugCategoriesList.Where(d => d.CategoryName == "Painkiller" ).ToList()
                },
            };
            return drugInformations;
        }
        
        public IEnumerable<DrugCategory> GetDrugCategories()
        {
            var drugCategories = new List<DrugCategory>()
            {
                new DrugCategory()
                {
                    CategoryName = "Antipsychotic",
                    Description = "This is antipsychotic drugs"
                },
                new DrugCategory()
                {
                    CategoryName = "Anxiolytic",
                    Description = "This is anxiolytic drugs"
                },
                new DrugCategory()
                {
                    CategoryName  = "Hypnotic",
                    Description = "This is hypnotic drugs"
                },
                new DrugCategory()
                {
                    CategoryName  = "Anticonvulsant",
                    Description = "This is anticonvulsant drugs"
                },
                new DrugCategory()
                {
                    CategoryName  = "Stimulant",
                    Description = "This is stimulant drugs"
                },
                new DrugCategory()
                {
                    CategoryName  = "Nootropic",
                    Description = "This is nootropic drugs"
                },
                new DrugCategory()
                {
                    CategoryName  = "Painkiller",
                    Description = "This is painkiller drugs"
                }
            };

            return drugCategories;
        }    
    }

}



        
        