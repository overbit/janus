using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace overapp.janus.Attributes
{
    public class StringIsoCurrencyCodeAttribute : ValidationAttribute
    {
        private readonly List<string> validCurrencies;

        public StringIsoCurrencyCodeAttribute()
        {
            var regions = CultureInfo.GetCultures(CultureTypes.AllCultures)
                                    .Where(c => !c.IsNeutralCulture)
                                    .Select(culture => {
                                        try
                                        {
                                            return new RegionInfo(culture.Name);
                                        }
                                        catch
                                        {
                                            return null;
                                        }
                                    }).Where(info => info != null);
            validCurrencies = regions.Select(info => info.ISOCurrencySymbol).ToList();
        }

        public override bool IsValid(object value)
        {
            var stringValue = value.ToString().ToUpperInvariant();

            if (string.IsNullOrWhiteSpace(stringValue) ||
                stringValue.Length != 3 ||
                !validCurrencies.Contains(stringValue))
            {
                return false;
            }

            return true;
        }
    }
}
