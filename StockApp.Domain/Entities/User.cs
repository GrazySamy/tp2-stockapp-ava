namespace StockApp.Domain.Entities
{
    public class User
    {
        #region Atributos
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Role { get; set; }
        #endregion
    }
}