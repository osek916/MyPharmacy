using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Pharmacies.Any())
                {
                    var pharmacies = GetPharmacy();
                    _dbContext.Pharmacies.AddRange(pharmacies);
                    _dbContext.SaveChanges();
                }

                //if(!_dbContext.Status())
                
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

        private IEnumerable<Pharmacy> GetPharmacy()
        {
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
                            DrugsName = "Medikinet",
                            SubstancesName = "Metylofenidat",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 20,
                            AmountOfPackages = 7,
                            DrugCategory = "Stimulant",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 10
                        },
                        new Drug()
                        {
                            DrugsName = "Xanax",
                            SubstancesName = "Alprazolam",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 1,
                            AmountOfPackages = 4,
                            DrugCategory = "Anxiolytic",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 15
                        },
                        new Drug()
                        {
                            DrugsName = "Stilnox",
                            SubstancesName = "Zolpidem",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 10,
                            AmountOfPackages = 11,
                            DrugCategory = "Hypnotic",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 20
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
                            DrugsName = "Medikinet",
                            SubstancesName = "Metylofenidat",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 20,
                            AmountOfPackages = 7,
                            DrugCategory = "Stimulant",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 10
                        },
                        new Drug()
                        {
                            DrugsName = "Nootropil",
                            SubstancesName = "Piracetam",
                            NumberOfTablets = 60,
                            MilligramsPerTablets = 1200,
                            AmountOfPackages = 8,
                            DrugCategory = "Nootropic",
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            Price = 21
                        },
                        new Drug()
                        {
                            DrugsName = "Imovane",
                            SubstancesName = "Zopiklon",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 7,
                            AmountOfPackages = 18,
                            DrugCategory = "Hypnotic",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 20
                        },
                        new Drug()
                        {
                            DrugsName = "Apap",
                            SubstancesName = "Paracetamol",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 500,
                            AmountOfPackages = 24,
                            DrugCategory = "Painkiller",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 20
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
                            DrugsName = "Noopeptil",
                            SubstancesName = "Noopept",
                            NumberOfTablets = 10,
                            MilligramsPerTablets = 10,
                            AmountOfPackages = 3,
                            DrugCategory = "Nootropic",
                            LumpSumDrug = false,
                            PrescriptionRequired = false,
                            Price = 13
                        },
                        new Drug()
                        {
                            DrugsName = "Relanium",
                            SubstancesName = "Diazepam",
                            NumberOfTablets = 30,
                            MilligramsPerTablets = 1,
                            AmountOfPackages = 4,
                            DrugCategory = "Anxiolytic",
                            LumpSumDrug = true,
                            PrescriptionRequired = true,
                            Price = 15
                        },
                        new Drug()
                        {
                            DrugsName = "Morfeo",
                            SubstancesName = "Zaleplon",
                            NumberOfTablets = 20,
                            MilligramsPerTablets = 10,
                            AmountOfPackages = 7,
                            DrugCategory = "Hypnotic",
                            LumpSumDrug = false,
                            PrescriptionRequired = true,
                            Price = 20
                        }
                    }
            }
            };

            return pharmacies;
        }

        /*
        public IEnumerable<DrugCategory> GetDrugCategories()
        {
            var drugCategories = new List<DrugCategory>()
            {
                new DrugCategory()
                {
                    Name = "Antipsychotic"
                },
                new DrugCategory()
                {
                    Name = "Anxiolytic"
                },
                new DrugCategory()
                {
                    Name = "Hypnotic"
                },
                new DrugCategory()
                {
                    Name = "Anticonvulsant"
                },
                new DrugCategory()
                {
                    Name = "Stimulant"
                },
                new DrugCategory()
                {
                    Name = "Nootropic"
                },
                new DrugCategory()
                {
                    Name = "Painkiller"
                }
            };

            return drugCategories;
        }
        */


    }
}



        
        