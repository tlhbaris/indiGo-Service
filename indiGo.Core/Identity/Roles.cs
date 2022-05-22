namespace indiGo.Core.Identity
{
    public static class Roles
    {
        public static readonly string Admin = "admin";
        public static readonly string Operator = "operator";
        public static readonly string Service = "service";
        public static readonly string Customer = "customer";
        public static readonly string Passive = "passive";

        public static readonly List<string> RoleList = new()
        {
            Admin,Operator,Service,Customer,Passive
        };

    }
}
 