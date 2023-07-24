﻿namespace Api.Entities
{
    public class Plan
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
    }
}
