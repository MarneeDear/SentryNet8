using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Extensions
{
    public static class StringExtensions
    {
        public static string GetHistoryFieldStatusClass(this string input)
        {
            switch (input) {
                case "Good":
                    return "goodDataHistory";
                case "Bad":
                    return "badDataHistory";
                case "Undefined":
                    return "undefinedDataHistory";
                case "Changed":
                    return "changedDataHistory";
                case "Enriched":
                    return "enrichedDataHistory";
                case "Not Provided":
                    return "notProvidedDataHistory";
                case "Source":
                    return "sourceDataHistory";
                default:
                    return "undefinedDataHistory";
            }
        }

        public static string GetHistoryFieldIconClass(this string input)
        {
            switch (input)
            {
                case "Good":
                    return "fa-check-square";
                case "Bad":
                    return "fa-ban";
                case "Undefined":
                    return "fa-exclamation-square";
                case "Changed":
                    return "fa-pen-square";
                case "Enriched":
                    return "fa-cog";
                case "Not Provided":
                    return "fa-exclamation-triangle";
                case "Source":
                    return "fa-server";
                default:
                    return "fa-exclamation-square";
            }
        }

        public static string GetStatusAttribute(this string input)
        {
            switch (input)
            {
                case "Good":
                    return "fa-check-square";
                case "Bad":
                    return "fa-ban";
                case "Changed":
                    return "fa-pen-square";
                case "Enriched":
                    return "fa-cog";
                case "Not Provided":
                    return "disabled readonly";
                case "Source":
                    return "fa-server";
                default:
                    return "fa-exclamation-square";
            }
        }
    }
}
