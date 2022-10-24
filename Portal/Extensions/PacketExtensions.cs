namespace Portal.Extensions
{
    public static class PacketExtensions
    {
        public static string GetPrice(this Packet packet) => String.Format("{0:€#,##0.00}", packet.Price);
        public static string GetCity(this Packet packet) => packet.City?.GetType().GetMember(packet.City.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!;
        public static string GetCanteen(this Packet packet) => packet.Canteen?.Location.ToString()!;
        public static string GetIsEightteenPlusPacket(this Packet packet) => ((bool)packet.IsEightteenPlusPacket!) ? "(18+)" : "";
        public static string GetMealType(this Packet packet) => packet.MealType?.GetType().GetMember(packet.MealType.ToString()!).First().GetCustomAttribute<DisplayAttribute>()!.GetName()!;
        public static string GetReservedBy(this Packet packet) => (packet.ReservedBy != null) ? packet.ReservedBy.Name! : "-";
        public static string GetLatestPickUpTime(this Packet packet) => new DateTime(packet.LatestPickUpTime!.Value.Year, packet.LatestPickUpTime!.Value.Month, packet.LatestPickUpTime!.Value.Day, packet.LatestPickUpTime!.Value.Hour, packet.LatestPickUpTime!.Value.Minute, packet.LatestPickUpTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL"));
        public static string GetPickUpDateTime(this Packet packet) => new DateTime(packet.PickUpDateTime!.Value.Year, packet.PickUpDateTime!.Value.Month, packet.PickUpDateTime!.Value.Day, packet.PickUpDateTime!.Value.Hour, packet.PickUpDateTime!.Value.Minute, packet.PickUpDateTime!.Value.Second).ToString("dddd d MMMM yyyy HH:mm", new CultureInfo("nl-NL"));
    }
}
