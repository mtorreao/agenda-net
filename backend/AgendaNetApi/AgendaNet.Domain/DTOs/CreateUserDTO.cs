namespace AgendaNet.Domain.DTOs;

public class CreateUserDTO
{
    public UserDTO? User { get; set; }
    public TokenDTO? Token { get; set; }
}