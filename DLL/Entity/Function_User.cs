namespace DLL.Entity
{
    public class Function_User
    {
        public string Function_UserId { get; set; }
        public string FunctionId { get; set; }
        public string UserId { get; set; }

        public Function Function { get; set; }
        public User User { get; set; }
    }
}
