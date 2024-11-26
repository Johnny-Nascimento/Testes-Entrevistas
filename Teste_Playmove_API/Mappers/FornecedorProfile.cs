using AutoMapper;
using Teste_Playmove_API.Entities;
using Teste_Playmove_API.Models;

namespace Teste_Playmove_API.Mappers
{
    public class FornecedorProfile : Profile
    {
        public FornecedorProfile() 
        {
            CreateMap<Fornecedor, FornecedorViewModel>();

            CreateMap<FornecedorInputModel, Fornecedor>();
        }
    }
}
