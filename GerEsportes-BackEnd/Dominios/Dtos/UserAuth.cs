namespace GerEsportes_BackEnd.Dominios.Dtos
{
    public class UserAuth
    {
        public UserAuth() { }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
