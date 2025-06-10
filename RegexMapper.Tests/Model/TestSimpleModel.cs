using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexMapper.Tests.Model;

public class TestSimpleModel : IEquatable<TestSimpleModel>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Value { get; set; }

    public bool Enabled { get; set; }

    public List<int> ExternalIds { get; set; }

    public bool Equals(TestSimpleModel other)
    {
        return other != null
               && this.Id.Equals(other.Id)
               && string.Equals(this.Name, other.Name)
               && this.Value.Equals(other.Value)
               && this.Enabled.Equals(other.Enabled)
               && this.ExternalIds.SequenceEqual(other.ExternalIds)
            ;
    }
}