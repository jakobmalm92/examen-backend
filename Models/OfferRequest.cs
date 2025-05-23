﻿namespace OfferApi.Models
{
    public class OfferRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? Service { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
