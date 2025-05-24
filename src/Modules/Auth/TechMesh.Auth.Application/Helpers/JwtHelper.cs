namespace TechMesh.Auth.Application.Helpers;

public static class JwtHelper
{
    public static RSA ExtractRsaKey(string pemKey)
    {
        if (string.IsNullOrWhiteSpace(pemKey))
            throw new ArgumentException("Key cannot be null or empty");

        var rsa = RSA.Create();

        string base64Key;
        byte[] keyBytes;

        if (pemKey.Contains("-----BEGIN PUBLIC KEY-----"))
        {
            base64Key = pemKey
                .Replace("-----BEGIN PUBLIC KEY-----", string.Empty)
                .Replace("-----END PUBLIC KEY-----", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Trim();

            keyBytes = Convert.FromBase64String(base64Key);
            rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);
        }
        else if (pemKey.Contains("-----BEGIN PRIVATE KEY-----"))
        {
            base64Key = pemKey
                .Replace("-----BEGIN PRIVATE KEY-----", string.Empty)
                .Replace("-----END PRIVATE KEY-----", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Trim();

            keyBytes = Convert.FromBase64String(base64Key);
            rsa.ImportPkcs8PrivateKey(keyBytes, out _);
        }
        else
        {
            throw new ArgumentException("Unsupported or malformed PEM key.");
        }

        return rsa;
    }
}