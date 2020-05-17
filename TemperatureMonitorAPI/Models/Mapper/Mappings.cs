using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMonitorAPI.Models;
using TemperatureMonitorAPI.Models.Dtos;
using AutoMapper;


namespace TemperatureMonitorAPI.Models.Mapper
{
    public class Mappings:Profile
    {

        public Mappings()
        {
            CreateMap<PatientDetail, PatientDetailDto>().ReverseMap();
            CreateMap<PatientDetail, PatientDetailCreateDto>().ReverseMap();
            CreateMap<TemperatureLogEntry , TemperatureLogEntryDto>().ReverseMap();
            CreateMap<TemperatureLogEntry , TemperatureLogEntryCreateDto>().ReverseMap();
        }

    }
}
