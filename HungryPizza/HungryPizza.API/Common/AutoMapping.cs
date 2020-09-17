using AutoMapper;
using HungryPizza.API.VO;
using HungryPizza.Domain.Models;

namespace HungryPizza.API.Common
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //CreateMap<ExtratoRequest, ExtratoRequestCore>().BeforeMap((source, target) =>
            //{
            //    source.Agencia = source.Agencia.PadLeft(5, '0');
            //});

            //CreateMap<ExtratoRequest, ConciliacaoRequestCore>().BeforeMap((source, target) =>
            //{
            //    source.Agencia = source.Agencia.PadLeft(5, '0');
            //});

            CreateMap<RequestClient, Client>();
        }
    }
}
