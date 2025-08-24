using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Types;

public record AuthServerOptions(string AuthorityUrl, string Realm, Dictionary<string, string> Scopes);