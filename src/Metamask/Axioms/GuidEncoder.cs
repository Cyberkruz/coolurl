using System;

namespace Metamask.Axioms
{
    /// <summary>
    /// Quick helper class to convert Guids to and from their
    /// base64 format. Used to make the url's just a bit smaller
    /// for the users.
    /// </summary>
    public static class GuidEncoder
    {
        /// <summary>
        /// Encodes a guid into a smaller string using Base64.
        /// </summary>
        /// <param name="guid">The guid to shorten.</param>
        /// <returns>A base64 encoded string.</returns>
        public static string Encode(Guid guid)
        {
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        /// <summary>
        /// Decodes a small string from Encode back into a Guid.
        /// </summary>
        /// <param name="value">The string to process.</param>
        /// <returns>A parsed Guid. Throws an exception if unable to parse.</returns>
        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }
    }
}
