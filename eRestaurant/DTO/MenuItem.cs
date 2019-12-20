namespace eRestaurant.DTO
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public double Portion { get; set; }
        public string Unit { get; set; }
        public string CookingTime { get; set; }
        public string Type { get; set; }
        public double Rating { get; set; }
        public byte[] Image { get; set; }
    }
}
