using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LimitOrderBook.Test;

public class PasswordTests
{
    [Fact]
    public void PasswordMustContainUpperCharacter()
    {
        var passwordVerifyer = UtilityClass.GetPasswordVerifyer();
        string password = "helloworld1";

        Assert.False(passwordVerifyer.PasswordIsValid(password));
    }

    [Fact]
    public void PasswordMustContainLowerCharacter()
    {
        var passwordVerifyer = UtilityClass.GetPasswordVerifyer();
        string password = "HELLOWORLD1";

        Assert.False(passwordVerifyer.PasswordIsValid(password));
    }

    [Fact]
    public void PasswordMustContainNumber()
    {
        var passwordVerifyer = UtilityClass.GetPasswordVerifyer();
        string password = "HelloWorld";

        Assert.False(passwordVerifyer.PasswordIsValid(password));
    }

    [Fact]
    public void PasswordMustHave8CharactersOrMore()
    {
        var passwordVerifyer = UtilityClass.GetPasswordVerifyer();
        string password = "Hw1";

        Assert.False(passwordVerifyer.PasswordIsValid(password));
    }

    [Fact]
    public void ValidPasswordGetsAccepted()
    {
        var passwordVerifyer = UtilityClass.GetPasswordVerifyer();
        string password = "HelloWorld1";

        Assert.True(passwordVerifyer.PasswordIsValid(password));
    }

}
