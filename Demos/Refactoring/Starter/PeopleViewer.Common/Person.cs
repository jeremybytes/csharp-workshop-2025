﻿namespace PeopleViewer.Common;

public record Person(int Id, string GivenName, string FamilyName,
    DateTime StartDate, int Rating, string FormatString = "") : IEquatable<Person>
{
    public override string ToString()
    {
        if (string.IsNullOrEmpty(FormatString))
            return $"{GivenName} {FamilyName}";
        return string.Format(FormatString, GivenName, FamilyName);
    }

    public virtual bool Equals(Person? other)
    {
        return this.Id == other?.Id;
    }

    
    public override int GetHashCode()
    {
        return this.Id;
    }
}
