using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace SantoshGantiHomeLab.Models
{
    /// <summary>
    /// This specifices the pet status.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PetStatus
    {
        /// <summary>
        /// Identifies as "available".
        /// </summary>
        [EnumMember(Value = "available")]
        Available = 1,

        /// <summary>
        /// Identifies as "pending".
        /// </summary>
        [EnumMember(Value = "pending")]
        Pending = 2,

        /// <summary>
        /// Identifies as "sold".
        /// </summary>
        [EnumMember(Value = "sold")]
        Sold = 3
    }
}
