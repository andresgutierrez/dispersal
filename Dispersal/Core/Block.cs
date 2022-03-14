
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;

namespace Dispersal.Core;

public sealed class Block
{
    public int Index { get; set; } = 0;

    public int Nonce { get; set; } = 0;

    public int TimeStamp { get; set; }

    public string PreviousHash { get; set; }

    public string Hash { get; set; }

    public List<Transaction> Transactions { get; set; }

    public Block(string previousHash, int timeStamp, List<Transaction> transactions)
    {
        TimeStamp = timeStamp;
        PreviousHash = previousHash;
        Transactions = transactions;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        SHA256 sha256 = SHA256.Create();

        string data = JsonSerializer.Serialize(Transactions);

        byte[] inputBytes = Encoding.UTF8.GetBytes($"{TimeStamp}-{PreviousHash}-{data}-{Nonce}");
        byte[] outputBytes = sha256.ComputeHash(inputBytes);

        return Utils.BytesToHex(outputBytes);
    }

    public void Mine(int difficulty)
    {
        var leadingZeros = new string('0', difficulty);

        while (Hash == null || Hash.Substring(0, difficulty) != leadingZeros)
        {
            Nonce++;
            Hash = CalculateHash();
        }
    }
}
