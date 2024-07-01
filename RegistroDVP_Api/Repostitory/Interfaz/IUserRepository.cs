using RegistroDVP_Api.Models.Dominio;

namespace RegistroDVP_Api.Repostitory.Interfaz
{
    public interface IUserRepository
    {
        Task<ResponseDto> InsertPersonAndUser(PersonDto dto);
        Task<ResponseDto> Login(LoginDto dto);
    }
}
