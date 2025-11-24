using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ConfigValidatorTool
{
    public class ValidationResult
    {
        public List<string> Errors { get; set; } = new();
    }

    public static class ConfigValidator
    {
        public static ValidationResult Validate(JObject config)
        {
            var result = new ValidationResult();

            // Required fields
            var requiredFields = new List<string>
            {
                "apiKey",
                "apiUrl",
                "timeout",
                "retries"
            };

            foreach (var field in requiredFields)
            {
                if (!config.ContainsKey(field))
                    result.Errors.Add($"Missing required field: {field}");
            }

            // Validate URL format
            if (config.ContainsKey("apiUrl"))
            {
                var url = config["apiUrl"]!.ToString();
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    result.Errors.Add("apiUrl is not a valid URL.");
            }

            // Validate timeout
            if (config.ContainsKey("timeout") && config["timeout"]!.Type != JTokenType.Integer)
                result.Errors.Add("timeout must be an integer.");

            // Validate retries
            if (config.ContainsKey("retries") && config["retries"]!.Type != JTokenType.Integer)
                result.Errors.Add("retries must be an integer.");

            return result;
        }
    }
}
