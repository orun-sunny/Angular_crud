using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Asp.netcore_with_angular.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        [JsonConverter(typeof(NumberOrStringConverter))]
        public string Price { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(FlexibleNullableIntConverter))]
        public int? Rating { get; set; }

        [JsonConverter(typeof(FlexibleBoolConverter))]
        public bool Status { get; set; }
    }

    public sealed class NumberOrStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.TryGetDecimal(out var value)
                    ? value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    : reader.GetDouble().ToString(System.Globalization.CultureInfo.InvariantCulture),
                JsonTokenType.String => reader.GetString() ?? string.Empty,
                JsonTokenType.Null => string.Empty,
                _ => throw new JsonException($"Cannot convert {reader.TokenType} to Price.")
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

    public sealed class FlexibleNullableIntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var intValue))
                {
                    return intValue;
                }

                if (reader.TryGetDouble(out var doubleValue))
                {
                    return (int)Math.Round(doubleValue);
                }
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var text = reader.GetString();
                if (string.IsNullOrWhiteSpace(text))
                {
                    return null;
                }

                if (int.TryParse(text, out var parsedInt))
                {
                    return parsedInt;
                }

                if (double.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var parsedDouble))
                {
                    return (int)Math.Round(parsedDouble);
                }
            }

            throw new JsonException($"Cannot convert {reader.TokenType} to Rating.");
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    public sealed class FlexibleBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True) return true;
            if (reader.TokenType == JsonTokenType.False) return false;

            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.TryGetInt32(out var number) && number != 0;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var text = reader.GetString()?.Trim();
                if (string.Equals(text, "true", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(text, "active", StringComparison.OrdinalIgnoreCase) ||
                    text == "1")
                {
                    return true;
                }

                if (string.Equals(text, "false", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(text, "inactive", StringComparison.OrdinalIgnoreCase) ||
                    text == "0")
                {
                    return false;
                }
            }

            throw new JsonException($"Cannot convert {reader.TokenType} to Status.");
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }
}
