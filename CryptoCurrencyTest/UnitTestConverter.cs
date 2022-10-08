using System;
using CryptoCurrency;
using Xunit;

namespace CryptoCurrencyTest;

public class UnitTestConverter
{
    private Converter SetupSut()
    {
        var sut = new Converter();
        sut.SetPricePerUnit("Bitcoin", 19500.43);
        sut.SetPricePerUnit("Ethereum", 1327.38);
        sut.SetPricePerUnit("BNB", 280.44);
        sut.SetPricePerUnit("Cardano", 0.4244);
        return sut;
    }

    [Fact]
    public void Test_Set_Existing_Price()
    {
        var sut = SetupSut();

        sut.SetPricePerUnit("Bitcoin", 19000.43);
        var result = sut.Convert("Bitcoin", "Ethereum", 1000);
        
        Assert.Equal(14314.235561783362, result);
    }

    [Fact]
    public void Test_Convert_NonExisting()
    {
        var sut = SetupSut();

        Assert.Throws<Exception>(() => sut.Convert("BitcoinNew", "Ethereum", 1000));
    }

    [Fact]
    public void Test_Simple_Conversion()
    {
        var sut = SetupSut();

        var result = sut.Convert("Bitcoin", "Ethereum", 1000);
        
        Assert.Equal(14690.917446398167, result);
    }

    [Theory]
    [InlineData("Bitcoin","Ethereum", -1000)]
    [InlineData("Bitcoin","Ethereum", 0)]
    [InlineData("Bitcoin","Ethereum", 10)]
    [InlineData("Bitcoin","Ethereum", 10000)]
    [InlineData("Bitcoin","Cardano", 1500)]
    [InlineData("BNB","Cardano", 1500)]
    [InlineData("Cardano","Ethereum", 1500)]
    public void Test_Both_Ways(string fromCurrency, string toCurrency, double amount)
    {
        var sut = SetupSut();

        var result = sut.Convert(fromCurrency, toCurrency, amount);
        var resultBack = sut.Convert(toCurrency, fromCurrency, result);
        
        Assert.Equal(amount, resultBack);
    }
}