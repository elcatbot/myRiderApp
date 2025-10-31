namespace myRideApp.Drivers.Application.Queries;

public partial record ListAvailableDriversQuery : IParsable<ListAvailableDriversQuery>
{
    public static ListAvailableDriversQuery Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
        {
            return result;
        }
        throw new FormatException($"Unable to parse '{s}' into ListAvailableDriversQuery.");
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ListAvailableDriversQuery result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }
        try
        {
            var parts = s.Split("&");
            var query = new ListAvailableDriversQuery();
            foreach (var part in parts)
            {
                var kv = part.Split('=');
                if (kv.Length != 2) continue;

                var key = kv[0].Trim().ToLowerInvariant();
                var value = kv[1].Trim();

                switch (key)
                {
                    case "latitude":
                        query.Latitude = Convert.ToDouble(value);
                        break;
                    case "longitude":
                        query.Longitude = Convert.ToDouble(value);
                        break;
                    case "radiuskm":
                        query.RadiusKm = Convert.ToDouble(value);
                        break;
                    case "time":
                        query.Time = Convert.ToDateTime(value);
                        break;
                    case "pagesize":
                        query.PageSize = Convert.ToInt32(value);
                        break;
                    case "page":
                        query.Page = Convert.ToInt32(value);
                        break;
                    case "sortby":
                        query.SortBy = value;
                        break;
                    case "descending":
                        query.Descending = Convert.ToBoolean(value);
                        break;
                }
            }
            result = query;
            return true;
        }
        catch
        {
            return false;
        }
    }
}