namespace AdvertBoard.Tests.Application.AppServicesTests.Data; 

public class DataHelper
{
    public static IEnumerable<object[]> GetInvalidPhones()
    {
        return new List<object[]>
        {
            new object[] { "+" },
            new object[] { "12345" },
            new object[] { "+1" },
            new object[] { "+72345678901234567890" },
            new object[] { "723-456-7890" },
            new object[] { "+7 (234) ABC-7890" },   
            new object[] { "+7 (234) ABC-7890" },   
            new object[] { "+7 (234) 567-89@0" },
            new object[] { "++7-234-567-8900" },
            new object[] { "+7-234-567-89" },
            new object[] { "" },
            new object[] { "abcdefghij" },
            new object[] { "+7 234 5678 9000" },
            new object[] { "+7 (234-567-8900" },
            new object[] { "+7 234)567-8900(" },
            new object[] { "+7--234-567-8900" },
            new object[] { "+7.234.5678..9000" }
        };
    }

    public static IEnumerable<object[]> GetValidEmails()
    {
        return new List<object[]>
        {
            new object[] { "simple@example.com" },
            new object[] { "user.name@example.co.uk" },
            new object[] { "user+tag@example.com" },
            new object[] { "mail123@example123.com" },
            new object[] { "firstname-lastname@example.com" },
            new object[] { "Delfina04@gmail.com" }
        };
    }

    public static IEnumerable<object[]> GetInvalidEmails()
    {
        return new List<object[]>
        {
            new object[] { "plainaddress" },
            new object[] { "@no-local-part.com" },
            new object[] { "Outlook Contact <outlook-contact@domain.com>" },
            new object[] { "no-at.domain.com" },
            new object[] { "no-tld@domain" },
            new object[] { ";beginning-semicolon@domain.co.uk" },
            new object[] { "middle-semicolon@domain.co;uk" },
            new object[] { "trailing-semicolon@domain.com;" },
            new object[] { "\"email+leading-quotes@domain.com" },
            new object[] { "email+middle\"-quotes@domain.com" },
            new object[] { "email+trailing-quotes\"@domain.com" },
            new object[] { "\"quoted-local-part\"@domain.com" },
            new object[] { "\"quoted@domain.com" },
            new object[] { "lots-of-dots@domain..gov..uk" },
            new object[] { "two-dots..in-local@domain.com" },
            new object[] { "multiple@domains@domain.com" },
            new object[] { "spaces in local@domain.com" },
            new object[] { "spaces-in-domain@dom ain.com" },
            new object[] { "pipe-in-domain@example.com|gov.uk" },
            new object[] { "comma,in-local@gov.uk" },
            new object[] { "comma-in-domain@domain,gov.uk" },
            new object[] { "pound-sign-in-local£@domain.com" },
            new object[] { "local-with-’-apostrophe@domain.com" },
            new object[] { "local-with-”-quotes@domain.com" },
            new object[] { "domain-starts-with-a-dot@.domain.com" },
            new object[] { "brackets(in)local@domain.com" }
        };
    }

    public static IEnumerable<object[]> GetValidPhones()
    {
        return new List<object[]>
        {
            new object[] { "+7 (123) 456-78-90" },
            new object[] { "+7 123 456 78 90" },
            new object[] { "+71234567890" },
            new object[] { "8 (123) 456-78-90" },
            new object[] { "8 123 456 78 90" },
            new object[] { "81234567890" },
            new object[] { "+7(123)456-78-90" },
            new object[] { "8(123)456-78-90" },
            new object[] { "+7 1234567890" },
            new object[] { "8 1234567890" }
        };
    }

    public static IEnumerable<object[]> GetValidPasswords()
    {
        return new List<object[]>
        {
            new object[] { "Password1!" },
            new object[] { "Abcdefg1@" },
            new object[] { "A1b2C3d4#" },
            new object[] { "StrongPass9$" },
            new object[] { "Valid#123" },
            new object[] { "TestPass8&" },
            new object[] { "GoodPass7*" }
        };
    }

    public static IEnumerable<object[]> GetInvalidPasswords()
    {
        return new List<object[]>
        {
            new object[] { "" },
            new object[] { "Short1!" },
            new object[] { "password1!" },
            new object[] { "PASSWORD1!" },
            new object[] { "Password!" },
            new object[] { "Password1" },
            new object[] { "Pass!" }
        };
    }
    
    public static IEnumerable<object[]> GetInvalidNames()
    {
        return new List<object[]>
        {
            new object[] { "12" },
            new object[] { "" },
            new object[] { "q" },
            new object[] { "qwe>" },
            new object[] { "jia1" },
            new object[] { "qwertyuiiopasfjvnasfajssfkamsc" },
            new object[] { "qwertyuiiopasfjs" },
            new object[] { "////" },
            new object[] { "я1вцла" }
        };
    }

    public static IEnumerable<object[]> GetValidNames()
    {
        return new List<object[]>
        {
            new object[] { "qwer" },
            new object[] { "KOAFAF" },
            new object[] { "лыац" },
            new object[] { "ЫвщОц" },
            new object[] { "ошфратмфвпшваф" }
        };
    }

    public static IEnumerable<object[]> GetInvalidLastnames()
    {
        return new List<object[]>
        {
            new object[] { "12" },
            new object[] { "qwe>" },
            new object[] { "jia1" },
            new object[] { "qwertyuiiopasfjvnasfajssfw" },
            new object[] { "////" },
            new object[] { "я1вцла" },
            new object[] { "я1вц ла" },
            new object[] { "        " }
        };
    }

    public static IEnumerable<object[]> GetValidLastnames()
    {
        return new List<object[]>
        {
            new object[] { "kis" },
            new object[] { "jiasasfasf" },
            new object[] { "ядчллфафуцер" },
            new object[] { "ЩлвфЛаьфцвJsdk" },
            new object[] { "" }
        };
    }
}
