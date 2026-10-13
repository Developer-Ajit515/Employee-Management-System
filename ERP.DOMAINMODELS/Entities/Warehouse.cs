using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Code { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<WarehouseInventory> WarehouseInventories
        {
            get;
            set;
        } = new List<WarehouseInventory>();
    }
}