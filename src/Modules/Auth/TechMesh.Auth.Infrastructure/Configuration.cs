﻿namespace TechMesh.Auth.Infrastructure;

public class Configuration
{
    public class JwtOptions
    {
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}