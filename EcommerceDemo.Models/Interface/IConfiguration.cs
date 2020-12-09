namespace EcommerceDemo.Models.Interface
{
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the continent size, which denotes both the width and the height of a continent.
        /// </summary>
        int ContinentSize { get; }

        /// <summary>
        /// Gets the HMAC SHA-512 key used to generate the verification hash of the web token.
        /// </summary>
        string WebTokenHMACKey { get; }

        /// <summary>
        /// Gets the AES-256 key used in the encryption of the web token data.
        /// </summary>
        string WebTokenAESKey { get; }
    }
}
