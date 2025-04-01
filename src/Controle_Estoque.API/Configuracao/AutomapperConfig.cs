using AutoMapper;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Estoques.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.API.Modulos.Movimentacoes.ViewModels;
using Controle_Estoque.API.Modulos.Produtos.ViewModels;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Movimentacoes;
using Controle_Estoque.Domain.Entidades.Produtos;
using Controle_Estoque.Domain.Enuns;

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

            CreateMap<EstoqueCreateViewModel, Estoque>()
             .ForMember(dest => dest.Id, opt => opt.Ignore()); // Garante que o ID será gerado no banco


             //.ForMember(dest => dest.TipoIdentificador, opt => opt.MapFrom(src => src.TipoIdentificador)); // Certifique-se de que o Enum está mapeado corretamente


            // aqui eu fiz um mapeamento pra pegar o nome da empresa
            CreateMap<Filial, FilialViewModel>()
               .ForMember(dest => dest.EmpresaNome, opt => opt.MapFrom(src => src.Empresa.Nome));


            CreateMap<Empresa, EmpresaViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            CreateMap<Filial, FilialViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            // precisei fazer pra garantir o update, ja que na base não tem Empresa nome, não passo ela
            // só é uma propiedade de visuazação
            CreateMap<Filial, FilialUpdateViewModel>().ReverseMap();

            CreateMap<Produto, ProdutoUpdateViewModel>().ReverseMap();

            CreateMap<Produto, ProdutoViewModel>().ReverseMap(); 

            CreateMap<Movimentacao, MovimentacaoViewModel>().ReverseMap();

            CreateMap<Estoque, EstoqueViewModel>().ReverseMap();





        }
    }
}
