using System.Diagnostics.CodeAnalysis;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Constants;

[ExcludeFromCodeCoverage]
public partial class Constants
{
    public static string PropIsMissing => $"Campo obrigatório.";
    
    public static string LengthRequiredToString(in string field, in int length) 
        => $"O {field} deve conter {length} caracteres.";
    
    public static string MinLengthRequiredToString(in string field, in int minLength) 
        => $"O {field} deve conter no mínimo {minLength} caracteres.";
    
    public static string MaxLengthRequiredToString(in string field, in int maxLength) 
        => $"O {field} deve conter no máximo {maxLength} caracteres.";
    
    public static string InvalidField(in string field) 
        => $"{field} inválido.";
    
    public static string AlreadyExistsField(in string field) 
        => $"{field} já cadastrado.";

    public static string InvalidDate => "Data inválida";
    public static string NegativeBilling => "A cobrança deve ser maior que 0";

}