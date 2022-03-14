
using Dispersal.Core;
using System.Text.Json;

Console.WriteLine("Dispersal");

int currentTime = Utils.GetCurrentTime();

Blockchain phillyCoin = new();


var block = new Block("", currentTime, );
//phillyCoin.AddBlock();

//phillyCoin.AddBlock(new Block("", currentTime, "{sender:MaHesh,receiver:Henry,amount:5}"));
//phillyCoin.AddBlock(new Block("", currentTime, "{sender:Mahesh,receiver:Henry,amount:5}"));

Console.WriteLine(phillyCoin.IsValid());
Console.WriteLine(JsonSerializer.Serialize(phillyCoin));
