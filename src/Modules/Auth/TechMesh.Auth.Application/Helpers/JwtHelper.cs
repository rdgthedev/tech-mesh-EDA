namespace TechMesh.Auth.Application.Helpers;

public static class JwtHelper
{
    public static RSA ExtractRsaPrivateKey(string privateKey)
    {
        if (string.IsNullOrEmpty(privateKey))
            throw new Exception("Private key cannot be null or empty");

        var rsa = RSA.Create();
        var privateKeyInBytes = Convert.FromBase64String(privateKey);
        rsa.ImportPkcs8PrivateKey(privateKeyInBytes, out _);

        return rsa;
    }

    public static RSA ExtractRsaPublicKey(string publicKey)
    {
        if (string.IsNullOrEmpty(publicKey))
            throw new Exception("Public key cannot be null or empty");

        var rsa = RSA.Create();
        var publicKeyInBytes = Convert.FromBase64String(publicKey);
        rsa.ImportSubjectPublicKeyInfo(publicKeyInBytes, out _);

        return rsa;
    }
}