using Microsoft.EntityFrameworkCore;
using RegistroDVP_Api.Models.DB;
using RegistroDVP_Api.Models.Dominio;
using RegistroDVP_Api.Repostitory.Interfaz;

namespace RegistroDVP_Api.Repostitory.Implementation
{
    public class UserRepostitory : IUserRepository
    {
        private readonly LoginDvpContext dbContext;
        public UserRepostitory(LoginDvpContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<ResponseDto> InsertPersonAndUser(PersonDto dto)
        {
            try
            {
                DateTime fecha = DateTime.Now;
                Persona? persona = new Persona();
                Usuario usuario = new Usuario();
                //insertar datos persona
                persona.Nombres = dto.Nombres;
                persona.Apellidos = dto.Apellidos;
                persona.NumeroIdentificacion = dto.NumeroIdentificacion;
                persona.Email = dto.Email;
                persona.TipoIdentificacion = dto.TipoIdentificacion;
                persona.FechaCreacion = fecha;
                await dbContext.Personas.AddAsync(persona);
                var res = await dbContext.SaveChangesAsync();
                if(res == 1)
                {
                    Persona? searchPerson = new Persona();

                    searchPerson = await dbContext.Personas.FirstOrDefaultAsync(p => p.Email == dto.Email);

                    //insertar datos usuario
                    if(searchPerson != null)
                    {
                        usuario.Identificador = searchPerson.Identificador;
                        usuario.Usuario1 = dto.Usuario;
                        usuario.Pass = dto.Pass;
                        usuario.FechaCreacion = fecha;
                        var response = await dbContext.Usuarios.AddAsync(usuario);
                        await dbContext.SaveChangesAsync();

                        return new ResponseDto("Insertado Correctamente", true);
                    }

                }
                return new ResponseDto("No se pudo insertar el registro", false);
            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false);
            }
        }
        public async Task<ResponseDto> Login(LoginDto dto)
        {
            try
            {
                DateTime fecha = DateTime.Now;
                Persona? persona = new Persona();
                Usuario? usuario = new Usuario();
                //insertar datos persona
                usuario = await dbContext.Usuarios.FirstOrDefaultAsync(p => p.Usuario1 == dto.Usuario);

                if (usuario != null)
                {
                    if (usuario.Pass.Equals(dto.Pass))
                    {
                        persona = await dbContext.Personas.FirstOrDefaultAsync(p => p.Identificador == usuario.Identificador);
                        if(persona != null)
                        {
                            var response = new
                            {
                                usuario = usuario.Usuario1,
                                email = persona.Email,
                                nombres = persona.Nombres,
                                apellidos = persona.Apellidos,
                                tipoIdentificacion = persona.TipoIdentificacion,
                                identificacionCompleta = persona.IdentificacionCompleta,
                                nombreCompleto = persona.NombreCompleto,
                                fechaCreacio = persona.FechaCreacion,
                            };
                            List<dynamic> lista = new List<dynamic>();
                            lista.Add(response);
                            return new ResponseDto("Logeo correcto", true, lista);

                        }
                    }
                    else
                    {
                        return new ResponseDto("Contraseña incorrecta", false);
                    }
                }

                return new ResponseDto("El nombre de usuario no existe", false);

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false);
            }
        }
    }
}
