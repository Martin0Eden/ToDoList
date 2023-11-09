namespace Domain.Entity
{
    public class Role
    {
        public string RoleId { get; set; }
        public string Name { get; set; }

        public Role()
        {}

        public Role(string id, string name)
        {
            RoleId = id;
            Name = name;
        }
    }
}
