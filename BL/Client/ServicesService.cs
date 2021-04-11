using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Dto;
using System.Data.Entity;
using AutoMapper;

namespace BL
{
    public class ServicesService
    {
        private readonly AllDbContext dbContext;
        private readonly MapperService mapper;
        private IEnumerable<Service> allServices;
        private IEnumerable<ServiceDto> allServicesDto;


        public ServicesService(AllDbContext dbContext, MapperService mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task ReloadAsync()
        {
            await dbContext.Services.LoadAsync();

            allServices = dbContext.Services.AsNoTracking().ToList();

            allServicesDto = allServices.Select(x => mapper.MapTo<Service, ServiceDto>(x));
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServices(string name = null)
        {
            if (allServicesDto == null)
                await ReloadAsync();

            if(name != null)
            {
                return allServicesDto.Where(x => x.Name.Contains(name));
            }

            return allServicesDto;
        }

    }
}
