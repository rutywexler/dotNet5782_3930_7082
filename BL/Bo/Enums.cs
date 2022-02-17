namespace BO

{
    public class Enums
    {
        public enum WeightCategories
        {
            Light = 1,
            Medium,
            Heavy
        }

        public enum Priorities
        {
            Regular = 1,
            Fast,
            Emergency
        }

        public enum PackageStatuses
        {
            CREATED,
            ASSOCIATED,
            COLLECTED,
            PROVIDED
        }

        public enum DroneStatus
        {
            Available,
            Meintenence,
            Delivery

        }

        public enum BatteryUsage { Available, Light, Medium, Heavy, Charging }
    }
}

