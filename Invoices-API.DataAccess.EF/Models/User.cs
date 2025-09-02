using System;
using System.Collections.Generic;

namespace Invoices_API.DataAccess.EF.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
