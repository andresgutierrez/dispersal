
namespace Dispersal.Core;

public sealed class Blockchain
{
    private readonly List<Block> chain = new();

    private List<Transaction> transactions = new();

    private const int Difficulty = 1;

    private const int Reward = 1;

    public IList<Block> Chain { get { return chain; } }

    public Blockchain()
    {
        AddGenesisBlock();
    }

    public Block CreateGenesisBlock()
    {
        Block block = new("", Utils.GetCurrentTime(), transactions);
        block.Mine(Difficulty);
        transactions = new List<Transaction>();
        return block;
    }

    public void AddGenesisBlock()
    {
        chain.Add(CreateGenesisBlock());
    }

    public Block GetLatestBlock()
    {
        return chain[chain.Count - 1];
    }

    public void CreateTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

    public void ProcessPendingTransactions(string minerAddress)
    {
        Block block = new(GetLatestBlock().Hash, Utils.GetCurrentTime(), transactions);
        AddBlock(block);

        transactions = new List<Transaction>();
        CreateTransaction(new Transaction("", minerAddress, Reward));
    }

    public void AddBlock(Block block)
    {
        Block latestBlock = GetLatestBlock();
        block.Index = latestBlock.Index + 1;
        block.PreviousHash = latestBlock.Hash;
        block.Mine(Difficulty);
        chain.Add(block);
    }

    public int GetBalance(string address)
    {
        int balance = 0;

        for (int i = 0; i < chain.Count; i++)
        {
            List<Transaction> transactions = chain[i].Transactions;

            for (int j = 0; j < transactions.Count; j++)
            {
                var transaction = transactions[j];

                //Console.WriteLine(transaction.FromAddress);

                if (transaction.FromAddress == address)
                    balance -= transaction.Amount;

                if (transaction.ToAddress == address)
                    balance += transaction.Amount;
            }
        }

        return balance;
    }

    public bool IsValid()
    {
        for (int i = 1; i < chain.Count; i++)
        {
            Block currentBlock = chain[i];
            Block previousBlock = chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
                return false;

            if (currentBlock.PreviousHash != previousBlock.Hash)
                return false;
        }
        return true;
    }
}

