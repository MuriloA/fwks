using System;
using System.Collections.Generic;
using System.Linq;

namespace FwksLabs.Libs.Core.Encoders;

public static class Base62Encoder
{
    private static string _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static void SetAlphabet(string alphabet)
    {
        if (string.IsNullOrWhiteSpace(alphabet) || alphabet.Length != alphabet.Distinct().Count())
            throw new ArgumentException("Alphabet must contain unique, non-empty characters.");

        _alphabet = alphabet;
    }

    public static string Encode(Guid guid)
    {
        var bytes = guid.ToByteArray();
        var base62 = ConvertBase(bytes.Select(b => (int)b).ToArray(), 256, _alphabet.Length);
        return string.Concat(base62.Select(i => _alphabet[i]));
    }

    public static Guid Decode(string input)
    {
        var values = input.Select(c => _alphabet.IndexOf(c)).ToArray();
        var bytes = ConvertBase(values, _alphabet.Length, 256)
            .Select(i => (byte)i).ToArray();

        var padded = new byte[16];
        Array.Copy(bytes, 0, padded, 16 - bytes.Length, bytes.Length);

        return new Guid(padded);
    }

    private static int[] ConvertBase(int[] source, int fromBase, int toBase)
    {
        var result = new List<int>();
        var leadingZeros = source.TakeWhile(x => x == 0).Count();

        while (source.Length > 0)
        {
            var quotient = new List<int>();
            var remainder = 0;

            foreach (var value in source)
            {
                var acc = value + remainder * fromBase;
                quotient.Add(acc / toBase);
                remainder = acc % toBase;
            }

            result.Insert(0, remainder);
            source = quotient.SkipWhile(x => x == 0).ToArray();
        }

        result.InsertRange(0, Enumerable.Repeat(0, leadingZeros));

        return result.ToArray();
    }
}