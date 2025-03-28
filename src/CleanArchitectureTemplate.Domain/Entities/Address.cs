﻿using CleanArchitectureTemplate.Domain.Entities.Base;

namespace CleanArchitectureTemplate.Domain.Entities;

public class Address : AuditableEntity
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
