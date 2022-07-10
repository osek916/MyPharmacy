using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{
    public interface IEmailSenderService
    {
        Task SendMessageToAllClients();
        //Task SendMessageToClient();
    }

    public class EmailSenderService : IEmailSenderService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public EmailSenderService(PharmacyDbContext dbContext, IMapper mapper, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task SendMessageToAllClients()
        {
            var pharmiaciesWithEmailParams = _dbContext
                .Pharmacies
                .Where(p => p.Id == _userContextService.PharmacyId)
                .Include(e => e.EmailParams)
                .AsNoTracking()
                .Single();
         
            

            Email email = new Email(pharmiaciesWithEmailParams.EmailParams);

            await email.Send("TYTUŁ",
                "ciało wiadomości",
                "email-docelowy.wp.pl");
        }

        /*
        public async Task SendMessageToClient()
        {
            var pharmaciesWithEmailParams = _dbContext
                .Pharmacies
                .Where(p => p.Id == _userContextService.PharmacyId)
                .Include(e => e.EmailParams)
                .AsNoTracking()

        }
        */
    }
}
