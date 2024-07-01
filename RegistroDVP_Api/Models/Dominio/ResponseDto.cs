namespace RegistroDVP_Api.Models.Dominio
{
    public class ResponseDto
    {
        public string? message { get; set; }
        public bool success { get; set;}
        public List<dynamic>? data { get; set; } = new List<dynamic>();
        public ResponseDto() { }
        public ResponseDto(string message,bool success, List<dynamic> lista = null) 
        { 
            this.message = message;
            this.success = success;
            this.data = lista;
        }
    }
}
