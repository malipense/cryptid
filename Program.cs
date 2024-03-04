// See https://aka.ms/new-console-template for more information
using Cryptid;

Option option = new Option();

if (args.Length == 0)
{
    Help();
    Environment.Exit(1);
}

for (int i = 0; i < args.Length; i++)
{
    if(args[i].StartsWith("--"))
    {
        switch (args[i])
        {
            case "--help":
                Help();
                Environment.Exit(1);
                break;
            case "--encrypt":
                option.Encrypt = true;
                break;
            case "--decrypt":
                option.Encrypt = false;
                break;
            case "--file":
                if(i + 1 > args.Length)
                {
                    Console.WriteLine("Value required");
                    Environment.Exit(1);
                }
                option.FilePath = args[i + 1];
                break;
            case "--type":
                if (i + 1 > args.Length)
                {
                    Console.WriteLine("Value required");
                    Environment.Exit(1);
                }
                option.Algorithm = args[i + 1];
                break;
            case "--key":
                if (i + 1 > args.Length)
                {
                    Console.WriteLine("Value required");
                    Environment.Exit(1);
                }
                option.Key = args[i + 1];
                break;
        }
    }
}

if (option.Encrypt && option.Algorithm == "aes")
{
    AESEncrypt(option.FilePath, option.Key);
}

if (!option.Encrypt && option.Algorithm == "aes")
{
    AESDecrypt(option.FilePath, option.Key);
}

void AESDecrypt(string file, string key)
{
    try
    {
        AES.Decrypt(file, key);
        Console.WriteLine("File decrypted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Environment.Exit(1);
    }
}

void AESEncrypt(string file, string key)
{
    try
    {
        AES.Encrypt(file, key);
        Console.WriteLine("File encrypted.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Environment.Exit(1);
    }
}

void Help()
{
    string helpMessage = $"Usage: cryptid.exe --encrypt --key eaWEqs457eqwsdqwe --type aes --file file.txt \n" +
        "--encrypt: encrypt file using the algorithm provided (--type) and the key (--key)\n" +
        "--decrypt: decrypt file using the algorithm provided (--type) and the key (--key)\n" +
        "\nAES requires a 32 character key - 256 bits 32 bytes";

    Console.WriteLine(helpMessage);
}

Console.WriteLine("Hello, World!");
