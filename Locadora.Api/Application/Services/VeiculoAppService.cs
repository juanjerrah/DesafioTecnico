using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Locadora.Api.Application.Interfaces;
using Locadora.Api.Application.ViewModels;
using Locadora.Api.Domain.Entities;
using Locadora.Api.Domain.Entities.Enums;
using Locadora.Api.Domain.Interfaces;
using Locadora.Api.Domain.Util;

namespace Locadora.Api.Application.Services;

public partial class VeiculoAppService : IVeiculoAppService
{
    private readonly IVeiculoRepository _repository;
    private readonly IMovimentacoesVeiculoRepository _repository2;
    private readonly IMapper _mapper;
    private readonly IMessageBus _bus;

    public VeiculoAppService(IVeiculoRepository repository, IMapper mapper, IMessageBus bus, IMovimentacoesVeiculoRepository repository2)
    {
        _repository = repository;
        _mapper = mapper;
        _bus = bus;
        _repository2 = repository2;
    }

    public async Task<IEnumerable<VeiculoResponse>> ObterVeiculos(VeiculoFiltroRequest filtro)
    {
        var predicate = ExpressionExtension.Start<Veiculo>();
        
        if (filtro.VeiculoId != null) 
            predicate = predicate.And(x => x.Id.Equals(filtro.VeiculoId));
        if (!string.IsNullOrWhiteSpace(filtro.Placa))
            predicate = predicate.And(x => x.Placa.ToLower().Equals(filtro.Placa.ToLower()));
        if (filtro.StatusVeiculo != null)
            predicate = predicate.And(x => x.StatusVeiculo.Equals(filtro.StatusVeiculo));
        if (filtro.TipoVeiculo != null)
            predicate = predicate.And(x => x.TipoVeiculo.Equals(filtro.TipoVeiculo));


        var veiculos = await _repository.ObterVeiculos(predicate);

        var x = _mapper.Map<IEnumerable<VeiculoResponse>>(veiculos);
        
        return x;
    }

    public async Task<VeiculoResponse> ObterVeiculoPorPlaca(string placa)
    {
        var veiculo = await _repository.ObterVeiculoPorPlaca(placa);

        if (veiculo == null)
        {
            _bus.RaiseValidationError($"Veículo de placa {placa} não consta na base", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }
        
        return _mapper.Map<VeiculoResponse>(veiculo);
    }

    public async Task<VeiculoResponse> InserirVeiculo(InserirVeiculoRequest veiculoRequest)
    {
        if (veiculoRequest.StatusDoVeiculo is null || veiculoRequest.TipoDoVeiculo is null)
        {
            _bus.RaiseValidationError("Tipo e status do veículo são obrigatórios", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }
        if (string.IsNullOrWhiteSpace(veiculoRequest.Placa))
        {
            _bus.RaiseValidationError("Placa é um campo obrigatório", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        var placaValida = ValidarPlaca(veiculoRequest.Placa);
        
        if (!placaValida)
        {
            _bus.RaiseValidationError($"A placa {veiculoRequest.Placa} é inválida. Formatos válidos: XXX0000 ou XXX0X00"
                , StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        var placaMercosul = ConverterPlacaMercosul(veiculoRequest.Placa);
        var veiculo = new Veiculo(Guid.NewGuid(), placaMercosul,
            veiculoRequest.TipoDoVeiculo!.Value, veiculoRequest.StatusDoVeiculo!.Value);

        await _repository.InserirVeiculo(veiculo);
        
        var movimentoVeiculo = new MovimentacoesVeiculo(
            $"Veiculo de placa {placaMercosul} foi inserído {veiculo.DateInc.ToString("f")}",
            EMovimentacaoVeiculo.EntradaVeiculoNaBase, veiculo.Id);

        await _repository2.InserirMovimentacaoVeiculo(movimentoVeiculo);
        
        return _mapper.Map<VeiculoResponse>(veiculo);

    }

    public async Task<VeiculoResponse> AtualizarVeiculo(AtualizarVeiculoRequest veiculoRequest)
    {
        if (string.IsNullOrWhiteSpace(veiculoRequest.Placa))
        {
            _bus.RaiseValidationError("Placa é um campo obrigatório", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        if (veiculoRequest.StatusVeiculo == null)
        {
            _bus.RaiseValidationError("Status do veículo é um campo obrigatório", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        var placaMercosul = ConverterPlacaMercosul(veiculoRequest.Placa);
        var veiculoBase = await _repository.ObterVeiculoPorPlaca(placaMercosul);

        if (veiculoBase == null)
        {
            _bus.RaiseValidationError($"Veículo de placa {placaMercosul} não encontrado na base", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        if (veiculoRequest.StatusVeiculo == veiculoBase.StatusVeiculo)
        {
            _bus.RaiseValidationError($"O Veículo de placa {placaMercosul} já se encontra na situação que deseja " +
                          $"alterar: {veiculoRequest.StatusVeiculo.ToString()}", StatusCodes.Status400BadRequest);
            return new VeiculoResponse();
        }

        var veiculo = new Veiculo(veiculoBase.Id, veiculoBase.Placa, veiculoBase.TipoVeiculo,
            veiculoRequest.StatusVeiculo.Value);

        await _repository.AtualizarVeiculo(veiculo);
        
        var mensagem = veiculoRequest.StatusVeiculo is EStatusVeiculo.Disponivel 
            ? $"Veiculo de placa {placaMercosul} está disponível desde: {veiculo.DateAlter.ToString("f")}" 
            : $"Veiculo de placa {placaMercosul} foi locado: {veiculo.DateAlter.ToString("f")}";
        var tipoMovimento = veiculoRequest.StatusVeiculo is EStatusVeiculo.Disponivel
            ? EMovimentacaoVeiculo.VeiculoRetornado
            : EMovimentacaoVeiculo.VeiculoAlugado;

        var movimentoVeiculo = new MovimentacoesVeiculo(mensagem, tipoMovimento, veiculoBase.Id);

        await _repository2.InserirMovimentacaoVeiculo(movimentoVeiculo);

        return _mapper.Map<VeiculoResponse>(veiculo);
    }

    public async Task<bool> ExcluirVeiculo(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
        {
            _bus.RaiseValidationError("Placa é um campo obrigatório", StatusCodes.Status400BadRequest);
            return false;
        }

        var placaMercosul = ConverterPlacaMercosul(placa);

        var veiculo = await _repository.ObterVeiculoPorPlaca(placaMercosul);
        
        if (veiculo == null)
        {
            _bus.RaiseValidationError($"Veículo de placa {placa} não encontrado na base", StatusCodes.Status400BadRequest);
            return false;
        }
        
        await _repository.RemoverVeiculo(veiculo);


        return true;
    }
}