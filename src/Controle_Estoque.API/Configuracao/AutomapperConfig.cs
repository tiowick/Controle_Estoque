using AutoMapper;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.API.Modulos.Movimentacoes.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Produtos;

namespace Controle_Estoque.API.Configuracao
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<EmpresaCreateViewModel, Empresa>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<FilialCreateViewModel, Filial>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProdutoCreateViewModel, Produto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<MovimentacaoCreateViewModel, Movimentacao>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());




            CreateMap<Empresa, EmpresaViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            CreateMap<Filial, FilialViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            CreateMap<Produto, ProdutoViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            CreateMap<Movimentacao, MovimentacaoViewModel>().ReverseMap();


        }
    }
}
