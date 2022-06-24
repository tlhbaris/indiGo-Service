namespace indiGo.Core.Identity
{
    public static class Roles
    {
        public static readonly string Admin = "ADMIN";
        public static readonly string Operator = "OPERATOR";
        public static readonly string Customer = "CUSTOMER";
        public static readonly string Passive = "PASSIVE";
        public static readonly string ElectricalService = "ELECTRICALSERVICE";
        public static readonly string GasService = "GASSERVICE";
        public static readonly string PlumbingService = "PLUMBINGSERVICE";



        public static readonly List<string> RoleList = new()
        {
            Admin,
            Operator,
            ElectricalService,
            GasService,
            PlumbingService,
            Customer,
            Passive
        };

    }
}
