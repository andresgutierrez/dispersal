
namespace Dispersal.Core;

public sealed class Blockchain
{
    private readonly List<Block> chain = new();

    private readonly List<Transaction> transactions = new();

    public IList<Block> Chain { get { return chain; } }

    public Blockchain()
    {
        AddGenesisBlock();
    }

    private static Block CreateGenesisBlock()
    {
        return new("", Utils.GetCurrentTime(), "{}");
    }

    private void AddGenesisBlock()
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

    public void AddBlock(Block block)
    {
        Block latestBlock = GetLatestBlock();
        block.Index = latestBlock.Index + 1;
        block.PreviousHash = latestBlock.Hash;
        block.Mine(2);
        chain.Add(block);
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

