namespace Domain.Entities;

using Domain.Entities.Fakers;
using Domain.Enums;

using FluentAssertions;

using Xunit;

public class FiltroTests
{
    [Theory]
    [InlineData(Tipo.Lista)]
    [InlineData(Tipo.Range)]
    [InlineData(Tipo.Valor)]
    public void ConverterParaTipoEspecifico(Tipo tipo)
    {
        Filtro filtro = FiltroFaker.GerarFiltro(tipo);

        ValidarFiltro(filtro);
    }

    #pragma warning disable xUnit1013
    public static void ValidarFiltro(Filtro filtro)
    {
        switch (filtro.Tipo)
        {
            case Tipo.Lista:
                ValidarTipoLista(filtro);
                break;
            case Tipo.Range:
                ValidarTipoRange(filtro);
                break;
            case Tipo.Valor:
                ValidarTipoValor(filtro);
                break;
        }
    }

    private static void ValidarTipoValor(Filtro filtro)
    {
        FiltroValor filtroConvertido = (FiltroValor)filtro;

        filtroConvertido.Nome.Should().NotBeNullOrEmpty();
        filtroConvertido.Valor.Should().NotBeNullOrEmpty();
    }

    private static void ValidarTipoRange(Filtro filtro)
    {
        FiltroRange filtroConvertido = (FiltroRange)filtro;

        filtroConvertido.Nome.Should().NotBeNullOrEmpty();
        filtroConvertido.Valor.De.Should().BePositive();
        filtroConvertido.Valor.Ate.Should().BePositive()
                                           .And
                                           .BeGreaterThan(filtroConvertido.Valor.De);
    }

    private static void ValidarTipoLista(Filtro filtro)
    {
        FiltroLista filtroConvertido = (FiltroLista)filtro;

        filtroConvertido.Nome.Should().NotBeNullOrEmpty();
        filtroConvertido.Valor.Should().NotBeNullOrEmpty();
    }
}